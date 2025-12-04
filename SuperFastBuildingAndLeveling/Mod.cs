using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using HarmonyLib;

namespace SuperFastBuildingAndLeveling
{
    public class SuperFastBuildingAndLeveling : IMod
    {

        private Harmony m_Harmony;
        public static ILog log = LogManager.GetLogger($"{nameof(SuperFastBuildingAndLeveling)}.{nameof(SuperFastBuildingAndLeveling)}").SetShowsErrorsInUI(false);
        public static Setting m_Setting;
        public static bool Ready = false;

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));


            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

            log.Info($"[{nameof(SuperFastBuildingAndLeveling)}] {nameof(OnLoad)} Injecting Harmony Patches.");
            m_Harmony = new Harmony("net.titangod.superfastbuildingandleveling");
            m_Harmony.PatchAll();

            m_Setting = new Setting(this);
            m_Setting.RegisterInOptionsUI();
            AssetDatabase.global.LoadSettings(nameof(SuperFastBuildingAndLeveling), m_Setting, new Setting(this));
            m_Setting.Contra = false;
            log.Info($"[{nameof(SuperFastBuildingAndLeveling)}] {nameof(OnLoad)} finished loading settings.");

            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));
            log.Info($"[{nameof(SuperFastBuildingAndLeveling)}] {nameof(OnLoad)} loaded localization for en-US.");

            Ready = true;

        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
            m_Harmony.UnpatchAll();
            if (m_Setting != null)
            {
                m_Setting.UnregisterInOptionsUI();
                m_Setting = null;
            }
        }
    }
}
