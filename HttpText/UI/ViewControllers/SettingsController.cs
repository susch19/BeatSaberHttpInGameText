using System.Collections.Generic;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Parser;
using HttpText.Models;
using UnityEngine;

namespace HttpText.UI.ViewControllers
{
    public class SettingsController : PersistentSingleton<SettingsController>
    {
        [UIParams]
        private BSMLParserParams parserParams;

        private Float3 _stdPos = new Float3();
        private Float3 _stdRot = new Float3();        
        private Float3 _menuPos = new Float3();
        private Float3 _menuRot = new Float3();
        private Float3 _noStdPos = new Float3();
        private Float3 _noStdRot = new Float3();

        [UIValue("enabled-bool")]
        public bool enabledValue
        {
            get => Plugin.config.Value.EnablePlugin;
            set => Plugin.config.Value.EnablePlugin = value;
        }

        [UIValue("std-panel-x-pos-float")]
        public float stdPanelXPosValue
        {
            get => Plugin.config.Value.HttpTextStandardLevelPosition.x;
            set => _stdPos = new Float3(value, _stdPos.y, _stdPos.z);
        }

        [UIValue("std-panel-y-pos-float")]
        public float stdPanelYPosValue
        {
            get => Plugin.config.Value.HttpTextStandardLevelPosition.y;
            set => _stdPos = new Float3(_stdPos.x, value, _stdPos.z);
        }

        [UIValue("std-panel-z-pos-float")]
        public float stdPanelZPosValue
        {
            get => Plugin.config.Value.HttpTextStandardLevelPosition.z;
            set => _stdPos = new Float3(_stdPos.x, _stdPos.y, value);
        }

        [UIValue("std-panel-x-rot-float")]
        public float stdPanelXRotValue
        {
            get => Plugin.config.Value.HttpTextStandardLevelRotation.x;
            set => _stdRot = new Float3(value, _stdRot.y, _stdRot.z);
        }

        [UIValue("std-panel-y-rot-float")]
        public float stdPanelYRotValue
        {
            get => Plugin.config.Value.HttpTextStandardLevelRotation.y;
            set => _stdRot = new Float3(_stdRot.x, value, _stdRot.z);
        }

        [UIValue("std-panel-z-rot-float")]
        public float stdPanelZRotValue
        {
            get => Plugin.config.Value.HttpTextStandardLevelRotation.z;
            set => _stdRot = new Float3(_stdRot.x, _stdRot.y, value);
        }


        [UIValue("menu-panel-x-pos-float")]
        public float menuPanelXPosValue
        {
            get => Plugin.config.Value.MenuTextStandardLevelPosition.x;
            set => _menuPos = new Float3(value, _menuPos.y, _menuPos.z);
        }

        [UIValue("menu-panel-y-pos-float")]
        public float menuPanelYPosValue
        {
            get => Plugin.config.Value.MenuTextStandardLevelPosition.y;
            set => _menuPos = new Float3(_menuPos.x, value, _menuPos.z);
        }

        [UIValue("menu-panel-z-pos-float")]
        public float menuPanelZPosValue
        {
            get => Plugin.config.Value.MenuTextStandardLevelPosition.z;
            set => _menuPos = new Float3(_menuPos.x, _menuPos.y, value);
        }

        [UIValue("menu-panel-x-rot-float")]
        public float menuPanelXRotValue
        {
            get => Plugin.config.Value.MenuTextStandardLevelRotation.x;
            set => _menuRot = new Float3(value, _menuRot.y, _menuRot.z);
        }

        [UIValue("menu-panel-y-rot-float")]
        public float menuPanelYRotValue
        {
            get => Plugin.config.Value.MenuTextStandardLevelRotation.y;
            set => _menuRot = new Float3(_menuRot.x, value, _menuRot.z);
        }

        [UIValue("menu-panel-z-rot-float")]
        public float menuPanelZRotValue
        {
            get => Plugin.config.Value.MenuTextStandardLevelRotation.z;
            set => _menuRot = new Float3(_menuRot.x, _menuRot.y, value);
        }


        [UIValue("no-std-panel-x-pos-float")]
        public float noStdPanelXPosValue
        {
            get => Plugin.config.Value.HttpText360LevelPosition.x;
            set => _noStdPos = new Float3(value, _noStdPos.y, _noStdPos.z);
        }

        [UIValue("no-std-panel-y-pos-float")]
        public float noStdPanelYPosValue
        {
            get => Plugin.config.Value.HttpText360LevelPosition.y;
            set => _noStdPos = new Float3(_noStdPos.x, value, _noStdPos.z);
        }

        [UIValue("no-std-panel-z-pos-float")]
        public float noStdPanelZPosValue
        {
            get => Plugin.config.Value.HttpText360LevelPosition.z;
            set => _noStdPos = new Float3(_noStdPos.x, _noStdPos.y, value);
        }

        [UIValue("no-std-panel-x-rot-float")]
        public float noStdPanelXRotValue
        {
            get => Plugin.config.Value.HttpText360LevelRotation.x;
            set => _noStdRot = new Float3(value, _noStdRot.y, _noStdRot.z);
        }

        [UIValue("no-std-panel-y-rot-float")]
        public float noStdPanelYRotValue
        {
            get => Plugin.config.Value.HttpText360LevelRotation.y;
            set => _noStdRot = new Float3(_noStdRot.x, value, _noStdRot.z);
        }

        [UIValue("no-std-panel-z-rot-float")]
        public float noStdPanelZRotValue
        {
            get => Plugin.config.Value.HttpText360LevelRotation.z;
            set => _noStdRot = new Float3(_noStdRot.x, _noStdRot.y, value);
        }

        [UIObject("std-pos-x-field")] public GameObject stdPosXField;
        [UIObject("std-pos-y-field")] public GameObject stdPosYField;
        [UIObject("std-pos-z-field")] public GameObject stdPosZField;
        [UIObject("std-rot-x-field")] public GameObject stdRotXField;
        [UIObject("std-rot-y-field")] public GameObject stdRotYField;
        [UIObject("std-rot-z-field")] public GameObject stdRotZField;
        [UIObject("no-std-pos-x-field")] public GameObject noStdPosXField;
        [UIObject("no-std-pos-y-field")] public GameObject noStdPosYField;
        [UIObject("no-std-pos-z-field")] public GameObject noStdPosZField;
        [UIObject("no-std-rot-x-field")] public GameObject noStdRotXField;
        [UIObject("no-std-rot-y-field")] public GameObject noStdRotYField;
        [UIObject("no-std-rot-z-field")] public GameObject noStdRotZField;

        private static void ResizeValuePicker(GameObject go)
        {
            if (go == null) return;
            var rectPicker = go.transform.Find("ValuePicker")?.GetComponent<RectTransform>();
            if (rectPicker)
                rectPicker.sizeDelta = new Vector2(25, rectPicker.sizeDelta.y);
        }

        [UIAction("#post-parse")]
        internal void Setup()
        {
            var list = new List<GameObject> {
                stdPosXField, stdPosYField, stdPosZField, stdRotXField, stdRotYField, stdRotZField,
                noStdPosXField, noStdPosYField, noStdPosZField, noStdRotXField, noStdRotYField, noStdRotZField
            };
            foreach (var go in list)
                ResizeValuePicker(go);
        }

        [UIAction("#apply")]
        public void OnApply()
        {
            Plugin.config.Value.EnablePlugin = enabledValue;
            Plugin.config.Value.HttpTextStandardLevelPosition = _stdPos;
            Plugin.config.Value.HttpTextStandardLevelRotation = _stdRot;
            Plugin.config.Value.HttpText360LevelPosition = _noStdPos;
            Plugin.config.Value.HttpText360LevelRotation = _noStdRot;
        }
    }
}
