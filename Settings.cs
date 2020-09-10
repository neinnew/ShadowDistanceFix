using System;
using System.IO;
using UnityEngine;
using System.Xml.Serialization;


namespace ShadowDistanceFix
{
    [XmlRoot(ElementName = "ShadowDistance", Namespace = "", IsNullable = false)]
    public class SDPSettingsFile
    {
        // Version.
        [XmlAttribute("version")]
        public int version = 0;

        // Max distance.
        [XmlElement("maxdistance")]
        public float maxDistance { get => SDPSettings.maxDistance; set => SDPSettings.maxDistance = value; }

        // Language.
        [XmlElement("mindistance")]
        public int minDistance { get => SDPSettings.minDistance; set => SDPSettings.minDistance = value; }
    }


    internal static class SDPSettings
    {
        private static readonly string SettingsFileName = "ShadowDistancePatch.xml";
        internal static float maxDistance = 40000f;
        internal static int minDistance = 1;


        /// <summary>
        /// Load settings from XML file.
        /// </summary>
        internal static void LoadSettings()
        {
            // Check to see if configuration file exists.
            if (File.Exists(SettingsFileName))
            {
                // Read it.
                using (StreamReader reader = new StreamReader(SettingsFileName))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(SDPSettingsFile));
                    if (!(xmlSerializer.Deserialize(reader) is SDPSettingsFile spdSettingsFile))
                    {
                        Debug.Log("Shadow Distance Patch: couldn't deserialize settings file");
                    }
                }
            }
        }


        /// <summary>
        /// Save settings to XML file.
        /// </summary>
        internal static void SaveSettings()
        {
            try
            {
                // Pretty straightforward.  Serialisation is within GBRSettingsFile class.
                using (StreamWriter writer = new StreamWriter(SettingsFileName))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(SDPSettingsFile));
                    xmlSerializer.Serialize(writer, new SDPSettingsFile());
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
