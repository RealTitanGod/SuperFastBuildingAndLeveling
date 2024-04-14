using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using HarmonyLib;

namespace InfiniteDemand
{
    public class InfiniteDemand : IMod
    {

        private Harmony m_Harmony;
        public static ILog log = LogManager.GetLogger($"{nameof(InfiniteDemand)}.{nameof(InfiniteDemand)}").SetShowsErrorsInUI(false);
        public static Setting m_Setting;

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));


            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

            log.Info($"[{nameof(InfiniteDemand)}] {nameof(OnLoad)} Injecting Harmony Patches.");
            m_Harmony = new Harmony("net.titangod.infinitedemand");
            m_Harmony.PatchAll();

            m_Setting = new Setting(this);
            m_Setting.RegisterInOptionsUI();
            AssetDatabase.global.LoadSettings(nameof(InfiniteDemand), m_Setting, new Setting(this));
            m_Setting.Contra = false;
            log.Info($"[{nameof(InfiniteDemand)}] {nameof(OnLoad)} finished loading settings.");

            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));
            log.Info($"[{nameof(InfiniteDemand)}] {nameof(OnLoad)} loaded localization for en-US.");


        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
            if (m_Setting != null)
            {
                m_Setting.UnregisterInOptionsUI();
                m_Setting = null;
            }
        }
    }
}
