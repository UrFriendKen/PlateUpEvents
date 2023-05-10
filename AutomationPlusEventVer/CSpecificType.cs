using KitchenData;
using Unity.Entities;

namespace KitchenAutomationPlusEventVer
{
    public struct CSpecificType : IComponentData
    {
        public int SpecificType;

        public ItemList SpecificComponents;
    }
}
