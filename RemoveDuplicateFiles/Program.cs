using System;
using System.Collections.Generic;
using System.IO;

namespace RemoveDuplicateFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = new Dictionary<string, FileInfo>();

            var root = new DirectoryInfo(args[0]);
            var folderToDeleteFrom = Path.GetFullPath(args[1]);

            foreach (FileInfo fileInfo in root.EnumerateFiles("*.*", SearchOption.AllDirectories))
            {
                string hash = $"{fileInfo.Name},{fileInfo.Length},{fileInfo.LastWriteTime.Ticks}";

                if (files.TryGetValue(hash, out var existingFileInfo))
                {
                    //Console.WriteLine($"Dup: {fileInfo.FullName} {existingFileInfo.FullName}");

                    FileInfo fileToDelete = null;
                    if (fileInfo.FullName.StartsWith(folderToDeleteFrom))
                    {
                        fileToDelete = fileInfo;
                    }
                    else if (existingFileInfo.FullName.StartsWith(folderToDeleteFrom))
                    {
                        fileToDelete = existingFileInfo;
                    }

                    if (fileToDelete != null)
                    {
                        Console.WriteLine(fileToDelete.FullName);
                        //fileToDelete.Delete();
                    }
                }
                files[hash] = fileInfo;
            }
        }
    }
}