using ICities;


namespace ShadowDistanceFix
{
    public class ShadowDistanceFix : LoadingExtensionBase, IUserMod
    {
        public string Name => "Shadow Distance Fix";
        public string Description => "Fixes shadow distance reduction due to Camera Positions Utility";

        private static readonly string[] OptionLabels =
        {
            "Very Sharpen",
            "Sharpen",
            "Vanilla",
            "Blurry",
            "Very Blurry"
        };

        private static readonly float[] OptionValues =
        {
            40,
            100,
            400,
            1000,
            4000
        };

        /// <summary>
        /// Called by the game when the mod is enabled.
        /// </summary>
        public void OnEnabled()
        {
            // Load the settings file.
            SDPSettings.LoadSettings();
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group = helper.AddGroup("Shadow Distance Fix");
            group.AddSlider("Shadow Distance", 6000, 40000, 1000f, SDPSettings.maxDistance, sel =>
            {
                var sdf = UnityEngine.Object.FindObjectOfType<CameraController>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (sdf != null)
                {
                    sdf.m_maxShadowDistance = sel;
                }

                // Update and save settings.
                SDPSettings.maxDistance = sel;
                SDPSettings.SaveSettings();
            });

            group.AddDropdown("minShadowDistance", OptionLabels, SDPSettings.minDistance, sel =>
            {
                var sdfmin = UnityEngine.Object.FindObjectOfType<CameraController>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (sdfmin != null)
                {
                    sdfmin.m_minShadowDistance = OptionValues[sel];
                }

                // Update and save settings.
                SDPSettings.minDistance = sel;
                SDPSettings.SaveSettings();
            });

        }
        public override void OnLevelLoaded(LoadMode mode)
        {
            var sdp = UnityEngine.Object.FindObjectOfType<CameraController>();
            sdp.m_maxShadowDistance = SDPSettings.maxDistance;
            sdp.m_minShadowDistance = OptionValues[SDPSettings.minDistance];
        }
    }
}