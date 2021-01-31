using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JimmyHaglund.Serialization {
    public static class FileManager {
        public static bool Save(object item, string exactPath) {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(exactPath, FileMode.Create)) {
                try {
                    formatter.Serialize(stream, item);
                }
                catch (Exception e) {
                    Console.Write("Error when saving to path " + exactPath + ": " + e);
                    return false;
                }
            }
            return true;
        }

        public static T Load<T>(string path) where T : class {
            return Load(path) as T;
        }

        public static object Load(string exactPath) {
            if (!FileExists(exactPath)) {
                Console.Write("File not found for path " + exactPath);
                return null;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            try {

                using (FileStream stream = new FileStream(exactPath, FileMode.Open)) {
                    try {
                        return formatter.Deserialize(stream);
                    }
                    catch (Exception) {
                        Console.Write("Error when loading object at path " + exactPath);
                        return null;
                    }
                }
            }
            catch {
                Console.Write("Could not open file stream at path " + exactPath + ". Please make sure that the path is correct.");
                return null;
            }
        }

        public static bool FileExists(string path) {
            return File.Exists(path);
        }

        public static bool DeleteFile(string exactPath) {
            if (!FileExists(exactPath)) return false;
            File.Delete(exactPath);
            if (File.Exists(exactPath + ".meta")) {
                File.Delete(exactPath + ".meta");
            }
            return true;
        }
    }
}
