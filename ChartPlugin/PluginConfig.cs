using HttpText.Models;

namespace HttpText
{
    internal class PluginConfig
    {
        public bool EnablePlugin = true;
        public Float3 HttpTextStandardLevelPosition = new Float3(0, 3.6f, 4f);
        public Float3 HttpTextStandardLevelRotation = new Float3(-35, 0, 0);
        public Float3 MenuTextStandardLevelPosition = new Float3(0, 3.6f, 4f);
        public Float3 MenuTextStandardLevelRotation = new Float3(-35, 0, 0);
        public Float3 HttpText360LevelPosition = new Float3(0, 3.5f, 4.2f);
        public Float3 HttpText360LevelRotation = new Float3(-35, 0, 0);
    }
}
