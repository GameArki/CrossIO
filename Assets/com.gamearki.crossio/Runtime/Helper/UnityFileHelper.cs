using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618

namespace GameArki.CrossIO {

    /// <summary>
    /// 读/写文件到 PersistentDataPath 和 StreamingAssetsPath
    /// 支持 iOS/Android/Windows
    /// </summary>
    internal static class UnityFileHelper {

        // ==== Write ====
        internal static void WriteFileBytesToStreamingAssets(string dir, string filename, byte[] content) {
            WriteFileBytes(Path.Combine(Application.streamingAssetsPath, dir), filename, content);
        }

        internal static void WriteFileBytesToPersistentDataPath(string dir, string filename, byte[] content) {
            WriteFileBytes(Path.Combine(Application.persistentDataPath, dir), filename, content);
        }

        internal static void WriteFileBytes(string dir, string filename, byte[] content) {
            dir = GetCrossDir(dir);
            bool has = Directory.Exists(dir);
            if (!has) {
                Directory.CreateDirectory(dir);
            }
            string path = Path.Combine(dir, filename);
            File.WriteAllBytes(path, content);
        }

        // ==== Read ====
        internal static Task<byte[]> ReadFileBytesFromStreamingAssetsAsync(string dir, string filename) {
            return ReadFileBytesAsync(Path.Combine(Application.streamingAssetsPath, dir), filename);
        }

        internal static Task<byte[]> ReadFileBytesFromPersistentDataPathAsync(string dir, string filename) {
            return ReadFileBytesAsync(Path.Combine(Application.persistentDataPath, dir), filename);
        }

        internal async static Task<byte[]> ReadFileBytesAsync(string dir, string filename) {
            dir = GetCrossDir(dir);
            var filePath = Path.Combine(dir, filename);
            try {
                UnityWebRequest request = UnityWebRequest.Get(filePath);
                request.SendWebRequest();//读取数据
                var down = request.downloadHandler;
                while (!down.isDone) {
                    await Task.Delay(10);
                }
                if (request.isHttpError || request.isNetworkError) {
                    System.Console.WriteLine("获取文件失败" + request.error);
                    return null;
                }
                return down.data;
            } catch (Exception ex) {
                System.Console.WriteLine("获取文件失败" + ex.ToString());
            }
            return null;
        }

        /// <summary>
        /// 根据需要将路径转换不同平台的路径
        /// </summary>
        static string GetCrossDir(string dir) {
            string path =
#if UNITY_ANDROID && !UNITY_EDITOR
            Path.Combine(dir); //安卓的dir已默认有"file://"
#elif UNITY_IOS && !UNITY_EDITOR
            Path.Combine("file://", dir);
#elif UNITY_STANDLONE_WIN || UNITY_EDITOR
            Path.Combine("file://", dir);
#else
            dir;
#endif
            return path;
        }

    }
}