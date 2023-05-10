using Kitchen;
using KitchenData;
using System.Security.Policy;
using Unity.Collections;
using Unity.Entities;

namespace KitchenAutomationPlusEventVer
{
    public class DestroyWronglyGrabbedItemsHotfix : DaySystem
    {
        EntityQuery Query;

        protected override void Initialise()
        {
            base.Initialise();
            Query = GetEntityQuery(typeof(CConveyPushItems), typeof(CItemHolder));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> entities = Query.ToEntityArray(Allocator.Temp);
            using NativeArray<CConveyPushItems> grabs = Query.ToComponentDataArray<CConveyPushItems>(Allocator.Temp);
            using NativeArray<CItemHolder> holders = Query.ToComponentDataArray<CItemHolder>(Allocator.Temp);

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                CConveyPushItems grab = grabs[i];
                CItemHolder holder = holders[i];

                if (!grab.Grab || grab.State != CConveyPushItems.ConveyState.Grab || !grab.GrabSpecificType || grab.SpecificType == 0 || holder.HeldItem == default ||
                    !Require(holder.HeldItem, out CItem item) || grab.SpecificType == item.ID)
                    continue;

                Main.LogWarning($"Smart Grabber grabbed item AFTER process completion. Destroying wrong item {item.ID}...");
                EntityManager.DestroyEntity(holder.HeldItem);
                holder.HeldItem = default;
                Set(entity, holder);
            }
        }
    }
}
