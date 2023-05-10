using Kitchen;
using KitchenData;
using KitchenLib;
using KitchenLib.Customs;
using KitchenLib.Utils;
using KitchenMods;
using PreferenceSystem;
using System;
using System.Reflection;
using Unity.Entities;
using UnityEngine;

// Namespace should have "Kitchen" in the beginning
namespace KitchenAutomationPlusEventVer
{
    public class Main : BaseMod, IModSystem
    {
        // GUID must be unique and is recommended to be in reverse domain name notation
        // Mod Name is displayed to the player and listed in the mods menu
        // Mod Version must follow semver notation e.g. "1.2.3"
        public const string MOD_GUID = "IcedMilo.PlateUp.AutomationPlusEventVer";
        public const string MOD_NAME = "AutomationPlus - Event Version";
        public const string MOD_VERSION = "0.1.3";
        public const string MOD_AUTHOR = "IcedMilo";
        public const string MOD_GAMEVERSION = ">=1.1.3";
        // Game version this mod is designed for in semver
        // e.g. ">=1.1.3" current and all future
        // e.g. ">=1.1.3 <=1.2.3" for all from/until

        // Boolean constant whose value depends on whether you built with DEBUG or RELEASE mode, useful for testing
#if DEBUG
        public const bool DEBUG_MODE = true;
#else
        public const bool DEBUG_MODE = false;
#endif
        public Main() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        static PreferenceSystemManager PrefManager;

        protected override void OnInitialise()
        {
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
            
            try
            {
                World.GetExistingSystem<InteractRotatePush>().Enabled = false;
            }
            catch (NullReferenceException)
            {
                LogInfo("Could not disable system Kitchen.InteractRotatePush!");
            }
        }

        protected override void OnUpdate()
        {
        }

        protected sealed override void OnPostActivate(Mod mod)
        {
            PrefManager = new PreferenceSystemManager(MOD_GUID, MOD_NAME);

            PrefManager
                .AddButtonWithConfirm("Make Space Outside", "Destroy ALL appliances outside and consolidate blueprints?", MakeSpaceOutsideCallback)
                .AddSpacer()
                .AddSpacer();

            PrefManager.RegisterMenu(PreferenceSystemManager.MenuType.PauseMenu);
        }

        protected void MakeSpaceOutsideCallback(GenericChoiceDecision decision)
        {
            if (Session.CurrentGameNetworkMode == GameNetworkMode.Host && decision == GenericChoiceDecision.Accept)
            {
                PreferenceActionController.MakeSpaceOutside();
            }
        }

        private static T1 GetModdedGDO<T1, T2>() where T1 : GameDataObject
        {
            return (T1)GDOUtils.GetCustomGameDataObject<T2>().GameDataObject;
        }
        private static T GetExistingGDO<T>(int id) where T : GameDataObject
        {
            return (T)GDOUtils.GetExistingGDO(id);
        }
        internal static T Find<T>(int id) where T : GameDataObject
        {
            return (T)GDOUtils.GetExistingGDO(id) ?? (T)GDOUtils.GetCustomGameDataObject(id)?.GameDataObject;
        }

        internal static T Find<T, C>() where T : GameDataObject where C : CustomGameDataObject
        {
            return GDOUtils.GetCastedGDO<T, C>();
        }

        internal static T Find<T>(string modName, string name) where T : GameDataObject
        {
            return GDOUtils.GetCastedGDO<T>(modName, name);
        }

        #region Logging
        public static void LogInfo(string _log) { Debug.Log($"[{MOD_NAME}] " + _log); }
        public static void LogWarning(string _log) { Debug.LogWarning($"[{MOD_NAME}] " + _log); }
        public static void LogError(string _log) { Debug.LogError($"[{MOD_NAME}] " + _log); }
        public static void LogInfo(object _log) { LogInfo(_log.ToString()); }
        public static void LogWarning(object _log) { LogWarning(_log.ToString()); }
        public static void LogError(object _log) { LogError(_log.ToString()); }
        #endregion
    }
}
