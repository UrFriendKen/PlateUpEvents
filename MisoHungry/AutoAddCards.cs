using Kitchen;
using KitchenCardsManager;
using KitchenLib.References;
using KitchenMods;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;

namespace KitchenMisoHungry
{
    public class AutoAddCards : RestaurantSystem
    {
        public struct SAdded: IComponentData, IModComponent { }

        HashSet<int> CardsToBeAdded => new HashSet<int> { UnlockReferences.MediumGroups, UnlockReferences.TreatFreeMoney };

        protected override void Initialise()
        {
            base.Initialise();
        }

        protected override void OnUpdate()
        {
            if (Has<SAdded>())
                return;

            foreach (int id in CardsToBeAdded)
            {
                CardsManagerUtil.AddProgressionUnlockToRun(id);
            }

            if (GameInfo.AllCurrentCards.Select(x => x.CardID).Intersect(CardsToBeAdded).Count() == CardsToBeAdded.Count)
                Set<SAdded>();
        }
    }
}
