using Kitchen;
using Unity.Entities;

namespace KitchenAutomationPlusEventVer
{
    [UpdateInGroup(typeof(InteractionGroup))]
    [UpdateBefore(typeof(ShowPingedApplianceInfo))]
    public class InteractRotatePushDuringNight : InteractRotatePush
    {
        protected override InteractionMode RequiredMode => InteractionMode.Appliances;

        protected override InteractionType RequiredType => InteractionType.Notify;
    }
}
