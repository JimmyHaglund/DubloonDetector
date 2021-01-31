using System.IO;
using UnityEngine;

namespace JimmyHaglund {
    /// <summary>
    /// Convenience class for handling saving and loading 
    /// files with root at Application.streamingAssetsPath
    /// </summary>
    /// The streaming assets path is used for storing assets to be streamed in at run time.
    /// Use this script to save data to be loaded during runtime.
    /// Note that StreamingAssetsPath is unavailable on WebGL and Android platforms.
    /// More info:
    /// https://docs.unity3d.com/ScriptReference/Application-streamingAssetsPath.html
    public static class SaveStreamingPath {

        public static string GetPath(string relativePath) {
            return Application.streamingAssetsPath + "/" + relativePath;
        }

        public static bool Save(object item, string path) {
            return FileManager.Save(item, GetPath(path));
        }

        public static T Load<T>(string path) where T : class {
            return Load(path) as T;
        }

        public static object Load(string path) {
            return FileManager.Load(GetPath(path));
        }

        public static bool FileExists(string path) {
            return File.Exists(GetPath(path));
        }

        public static bool DeleteFile(string path) {
            return FileManager.DeleteFile(GetPath(path));
        }
    }
}