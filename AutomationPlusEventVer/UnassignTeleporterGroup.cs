using Kitchen;
using Unity.Entities;

namespace KitchenAutomationPlusEventVer
{
    [UpdateBefore(typeof(ShowPingedApplianceInfo))]
    public class UnassignTeleporterGroup : InteractionSystem
    {
        protected override bool AllowAnyMode => true;
        protected override InteractionType RequiredType => InteractionType.Notify;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!(Has<SPracticeMode>() || Has<SIsNightTime>()))
            {
                return false;
            }
            if (!Require(data.Target, out CConveyTeleport teleport) || teleport.Target == default)
            {
                return false;
            }
            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            if (Require(data.Target, out CConveyTeleport teleport))
            {
                teleport.Target = default;
                teleport.GroupID = 0;
                Set(data.Target, teleport);
            }
        }
    }
}
