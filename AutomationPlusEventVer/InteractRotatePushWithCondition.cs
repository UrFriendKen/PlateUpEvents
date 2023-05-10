using Kitchen;

namespace KitchenAutomationPlusEventVer
{
    public class InteractRotatePushWithCondition : InteractRotatePush
    {
        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Has<SPracticeMode>())
            {
                return false;
            }
            return base.IsPossible(ref data);
        }
    }
}
