using System.IO;
using UnityEngine;

namespace JimmyHaglund {
    /// <summary>
    /// Convenience class for handling saving and loading 
    /// files with root at Application.dataPath
    /// </summary>
    /// The application data folder is suitable for storing save data.
    /// More info:
    /// https://docs.unity3d.com/ScriptReference/Application-dataPath.html
    public static class ProjectFileManager {
        public static string GetPath(string path) {
            return Application.dataPath + "/" + path;
        }

        public static bool Save(object item, string path) {
            // Save to file
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