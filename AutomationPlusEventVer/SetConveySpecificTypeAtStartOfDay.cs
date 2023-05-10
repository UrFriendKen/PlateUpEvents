using Kitchen;
using Unity.Entities;

namespace KitchenAutomationPlusEventVer
{
    public class SetSpecificTypeInhibitSystemRunningAtStartOfDay : StartOfDaySystem
    {
        protected override void Initialise()
        {
            base.Initialise();
        }

        protected override void OnUpdate()
        {
            if (!HasSingleton<SCreateScene>())
            {
                Set<SIsSpecificTypeInhibitSystemRunning>();
            }
        }
    }
}
