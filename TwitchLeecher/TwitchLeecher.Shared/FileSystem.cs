﻿using System.IO;

namespace TwitchLeecher.Shared
{
    public static class FileSystem
    {
        public static void ResetFileAttributes(string file)
        {
            if (File.Exists(file))
            {
                File.SetAttributes(file, FileAttributes.Normal);
            }
        }

        public static void DeleteFile(string file)
        {
            FileInfo fileInfo = new FileInfo(file);

            if (fileInfo.Exists)
            {
                ResetFileAttributes(fileInfo.FullName);
                fileInfo.Delete();
            }
        }

        public static void CreateDirectory(string directory)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(directory);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }

        public static void DeleteDirectory(string directory)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(directory);

            if (dirInfo.Exists)
            {
                CleanDirectory(directory);
                dirInfo.Delete(true);
            }
        }

        public static void CleanDirectory(string directory)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(directory);

            if (dirInfo.Exists)
            {
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    DeleteFile(file.FullName);
                }

                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {
                    DeleteDirectory(dir.FullName);
                }
            }
        }

        public static void CopyFile(string sourceFile, string targetDir, string newFileName = null)
        {
            CreateDirectory(targetDir);

            FileInfo fileInfo = new FileInfo(sourceFile);

            string targetFile = Path.Combine(targetDir, newFileName ?? fileInfo.Name);

            ResetFileAttributes(targetFile);

            File.Copy(fileInfo.FullName, targetFile, true);
        }
    }
}