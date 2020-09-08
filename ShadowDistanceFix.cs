using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowDistanceFix
{
    public class ShadowDistanceFix : LoadingExtensionBase, IUserMod
    {
        public string Name => "Shadow Distance Fix";
        public string Description => "Fixes shadow distance reduction due to Camera Positions Utility";

        private static readonly string[] OptionLabels =
        {
            "Sharpen",
            "Vanilla",
            "Blurry"
        };

        private static readonly float[] OptionValues =
        {
            40,
            400,
            4000
        };

        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group = helper.AddGroup("Shadow Distance Fix");
            group.AddSlider("Shadow Distance", 6000, 40000, 1000f, 40000f, sel =>
            {
                var sdf = UnityEngine.Object.FindObjectOfType<CameraController>();
                sdf.m_maxShadowDistance = sel;

            });

            group.AddDropdown("minShadowDistance", OptionLabels, 1, sel =>
            {
                var sdfmin = UnityEngine.Object.FindObjectOfType<CameraController>();
                sdfmin.m_minShadowDistance = OptionValues[sel];
            });

        }
        public override void OnLevelLoaded(LoadMode mode)
        {
            var sdp = UnityEngine.Object.FindObjectOfType<CameraController>();
            sdp.m_maxShadowDistance = 40000;
        }
    }
}
