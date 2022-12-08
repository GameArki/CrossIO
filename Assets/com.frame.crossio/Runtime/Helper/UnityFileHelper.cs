using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618

namespace JackFrame {

    public static class UnityFileHelper {

        public static void WriteFileBytes(string dir, string filename, byte[] content) {
            dir = CoverCrossDir(dir);
            bool has = Directory.Exists(dir);
            if (!has) {
                Directory.CreateDirectory(dir);
            }
            string path = Path.Combine(dir, filename);
            File.WriteAllBytes(path, content);
        }

        public static void WriteFileString(string dir, string filename, string content) {
            dir = CoverCrossDir(dir);
            bool has = Directory.Exists(dir);
            if (!has) {
                Directory.CreateDirectory(dir);
            }
            string path = Path.Combine(dir, filename);
            File.WriteAllText(path, content);
        }

        public async static Task<string> ReadFileString(string dir, string filename) {
            dir = CoverCrossDir(dir);
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
                return down.text;
            } catch (Exception ex) {
                System.Console.WriteLine("获取文件失败" + ex.ToString());
            }
            return null;
        }

        public async static Task<byte[]> ReadFileBytes(string dir, string filename) {
            dir = CoverCrossDir(dir);
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
        static string CoverCrossDir(string dir) {
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