using System.Collections;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.FloatingScreen;
using BeatSaberMarkupLanguage.Settings;
using BS_Utils.Utilities;
using IPA;
using IPA.Config;
using IPA.Utilities;
using HttpText.Core;
using HttpText.Models;
using HttpText.UI.ViewControllers;
using HttpText.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Config = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;

namespace HttpText
{
    public class Plugin : IBeatSaberPlugin
    {
        internal static Ref<PluginConfig> config;
        internal static IConfigProvider   configProvider;

        public void Init(IPALogger logger, [Config.Prefer("json")] IConfigProvider cfgProvider)
        {
            Logger.log = logger;
            logger.Debug("Loading HttpText Plugin");
            BSEvents.menuSceneLoadedFresh += OnMenuSceneLoadedFresh;
            BSEvents.menuSceneLoadedFresh += OnGameSceneActive;
            BSEvents.gameSceneLoaded += OnGameSceneActive;
            configProvider = cfgProvider;

            config = cfgProvider.MakeLink<PluginConfig>((p, v) => {
                config = v;
            });
        }

        private static void OnMenuSceneLoadedFresh()
        {
            BSMLSettings.instance.AddSettingsMenu("<size=75%>Http Text Settings</size>", "HttpText.UI.Views.settings.bsml", SettingsController.instance);
        }

        private static IEnumerator ShowFloating()
        {
            yield return new WaitForEndOfFrame();

            Logger.log.Debug("Showing Floating");
            var is360Level = BS_Utils.Plugin.LevelData?.GameplayCoreSceneSetupData?.difficultyBeatmap?.beatmapData?.spawnRotationEventsCount > 0;
            var pos = is360Level ? Float3.ToVector3(config.Value.HttpText360LevelPosition) : Float3.ToVector3(config.Value.HttpTextStandardLevelPosition);
            var rot = is360Level
                ? Quaternion.Euler(Float3.ToVector3(config.Value.HttpText360LevelRotation))
                : Quaternion.Euler(Float3.ToVector3(config.Value.HttpTextStandardLevelRotation));
            Logger.log.Debug("is 360 level: " + is360Level);
            var floatingScreen = FloatingScreen.CreateFloatingScreen(new Vector2(0, 0), false, pos, rot);
            Logger.log.Debug("new floating screen");
            floatingScreen.SetRootViewController(BeatSaberUI.CreateViewController<TextViewController>(), true);
            Logger.log.Debug("get image component");
            floatingScreen.GetComponent<Image>().enabled = false;
            Logger.log.Debug("add text creator component");
            floatingScreen.gameObject.AddComponent<TextCreator>();
            Logger.log.Debug("Floating should be shown by now");
        }

        private static void OnGameSceneActive()
        {
            Logger.log.Debug("Plugin will be activated: " + config.Value.EnablePlugin);
            if (config.Value.EnablePlugin)
                new UnityTask(ShowFloating());
        }

        public void OnApplicationStart() { }

        public void OnApplicationQuit() { }

        public void OnFixedUpdate() { }

        public void OnUpdate() { }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene) { }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) { }

        public void OnSceneUnloaded(Scene scene) { }
    }
}