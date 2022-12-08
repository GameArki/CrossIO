using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace JackFrame {

    /*
        修改此类的风险非常大
        请与主程或架构负责人联系

        修改此类的风险非常大
        请与主程或架构负责人联系

        修改此类的风险非常大
        请与主程或架构负责人联系

        修改此类的风险非常大
        请与主程或架构负责人联系

        修改此类的风险非常大
        请与主程或架构负责人联系

        修改此类的风险非常大
        请与主程或架构负责人联系

        修改此类的风险非常大
        请与主程或架构负责人联系
    */
    public static class FileHelper {

        // 找到某个文件
        public static string FindFileWithExt(string rootPath, string fileName, string ext) {
            List<string> all = FindAllFileWithExt(rootPath, ext);
            return all.Find(value => value.Contains(fileName + ext.TrimStart('*')));
        }

        // 递归
        public static List<string> FindAllFileWithExt(string rootPath, string ext) {

            List<string> fileList = new List<string>();

            DirectoryInfo directoryInfo = new DirectoryInfo(rootPath);
            FileInfo[] allFiles = directoryInfo.GetFiles(ext);
            for (int i = 0; i < allFiles.Length; i += 1) {
                var file = allFiles[i];
                fileList.Add(file.FullName);
            }

            DirectoryInfo[] childrenDirs = directoryInfo.GetDirectories();
            for (int i = 0; i < childrenDirs.Length; i += 1) {
                var dir = childrenDirs[i];
                fileList.AddRange(FindAllFileWithExt(dir.FullName, ext));
            }

            return fileList;

        }

        public static bool DirectoryCopy(string sourceDirectory, string targetDirectory) {
            try {
                DirectoryInfo dir = new DirectoryInfo(sourceDirectory);
                //获取目录下（不包含子目录）的文件和子目录
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
                foreach (FileSystemInfo i in fileinfo) {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (!Directory.Exists(Path.Combine(targetDirectory, i.Name))) {
                            //目标目录下不存在此文件夹即创建子文件夹
                            Directory.CreateDirectory(Path.Combine(targetDirectory, i.Name));
                        }
                        //递归调用复制子文件夹
                        DirectoryCopy(i.FullName, Path.Combine(targetDirectory, i.Name));
                    } else {
                        //不是文件夹即复制文件，true表示可以覆盖同名文件
                        File.Copy(i.FullName, Path.Combine(targetDirectory, i.Name), true);
                    }
                }
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public static void CreateDirIfNorExist(string _dirPath) {
            if (!Directory.Exists(_dirPath)) {
                Directory.CreateDirectory(_dirPath);
            }
        }

        public static void SaveFileText(string txt, string path) {

            using (StreamWriter sw = File.CreateText(path)) {
                sw.Write(txt);
            }

        }

        public static string LoadTextFromFile(string path) {

            using (StreamReader sr = new StreamReader(path)) {
                return sr.ReadToEnd();
            }

        }

        public static void DeleteAllFilesInDirUnsafe(string _dirPath) {

            string[] _files = Directory.GetFiles(_dirPath);
            for (int i = 0; i < _files.Length; i += 1) {
                string _path = _files[i];
                File.Delete(_path);
            }

        }

    }
}