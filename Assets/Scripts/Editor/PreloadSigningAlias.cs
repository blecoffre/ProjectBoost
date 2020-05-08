using UnityEngine;
using UnityEditor;
using System.IO;

namespace TrickyRocket.Editor
{
    [InitializeOnLoad]
    public class PreloadSigningAlias
    {
        /// <summary>
        /// Used to avoid having to enter this informations each time we open Unity and want to build the project for Android
        /// </summary>
        static PreloadSigningAlias()
        {
            PlayerSettings.Android.keystorePass = "beber7638";
            PlayerSettings.Android.keyaliasName = "trickyrocket";
            PlayerSettings.Android.keyaliasPass = "beber7638";
        }
    }
}

