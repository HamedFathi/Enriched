using Enriched.EnumerableExtended;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Enriched.DirectoryInfoExtended
{
    public static class DirectoryInfoExtensions
    {
        public static void Clear(this DirectoryInfo obj)
        {
            Array.ForEach(obj.GetFiles(), x => x.Delete());
            Array.ForEach(obj.GetDirectories(), x => x.Delete(true));
        }

        public static void CopyTo(this DirectoryInfo obj, string destDirName)
        {
            obj.CopyTo(destDirName, "*.*", SearchOption.TopDirectoryOnly);
        }

        public static void CopyTo(this DirectoryInfo obj, string destDirName, string searchPattern)
        {
            obj.CopyTo(destDirName, searchPattern, SearchOption.TopDirectoryOnly);
        }

        public static void CopyTo(this DirectoryInfo obj, string destDirName, SearchOption searchOption)
        {
            obj.CopyTo(destDirName, "*.*", searchOption);
        }

        public static void CopyTo(this DirectoryInfo obj, string destDirName, string searchPattern, SearchOption searchOption)
        {
            foreach (var file in obj.GetFiles(searchPattern, searchOption))
            {
                var outputFile = destDirName + file.FullName.Substring(obj.FullName.Length);
                var directory = new FileInfo(outputFile).Directory;

                if (directory == null)
                {
                    throw new Exception("The directory cannot be null.");
                }

                if (!directory.Exists)
                {
                    directory.Create();
                }

                file.CopyTo(outputFile);
            }
            foreach (var directory in obj.GetDirectories(searchPattern, searchOption))
            {
                var outputDirectory = destDirName + directory.FullName.Substring(obj.FullName.Length);
                var directoryInfo = new DirectoryInfo(outputDirectory);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }
            }
        }

        public static void CreateDirectory(this DirectoryInfo dirInfo)
        {
            if (dirInfo.Parent != null) CreateDirectory(dirInfo.Parent);
            if (!dirInfo.Exists) dirInfo.Create();
        }

        public static void DeleteDirectoriesWhere(this DirectoryInfo obj, Func<DirectoryInfo, bool> predicate)
        {
            obj.GetDirectories().Where(predicate).ForEach(x => x.Delete());
        }

        public static void DeleteDirectoriesWhere(this DirectoryInfo obj, SearchOption searchOption, Func<DirectoryInfo, bool> predicate)
        {
            obj.GetDirectories("*.*", searchOption).Where(predicate).ForEach(x => x.Delete());
        }

        public static int DeleteFiles(this DirectoryInfo di, string searchPattern)
        {
            return DeleteFiles(di, searchPattern);
        }

        public static int DeleteFiles(this DirectoryInfo di, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            int deleted = 0;
            foreach (FileInfo fi in di.GetFiles(searchPattern, searchOption))
            {
                fi.Delete();
                deleted++;
            }

            return deleted;
        }

        public static void DeleteFilesWhere(this DirectoryInfo obj, Func<FileInfo, bool> predicate)
        {
            obj.GetFiles().Where(predicate).ForEach(x => x.Delete());
        }

        public static void DeleteFilesWhere(this DirectoryInfo obj, SearchOption searchOption, Func<FileInfo, bool> predicate)
        {
            obj.GetFiles("*.*", searchOption).Where(predicate).ForEach(x => x.Delete());
        }

        public static void DeleteOlderThan(this DirectoryInfo obj, TimeSpan timeSpan)
        {
            DateTime minDate = DateTime.Now.Subtract(timeSpan);
            obj.GetFiles().Where(x => x.LastWriteTime < minDate).ToList().ForEach(x => x.Delete());
            obj.GetDirectories().Where(x => x.LastWriteTime < minDate).ToList().ForEach(x => x.Delete());
        }

        public static void DeleteOlderThan(this DirectoryInfo obj, SearchOption searchOption, TimeSpan timeSpan)
        {
            DateTime minDate = DateTime.Now.Subtract(timeSpan);
            obj.GetFiles("*.*", searchOption).Where(x => x.LastWriteTime < minDate).ToList().ForEach(x => x.Delete());
            obj.GetDirectories("*.*", searchOption).Where(x => x.LastWriteTime < minDate).ToList().ForEach(x => x.Delete());
        }

        public static DirectoryInfo EnsureDirectoryExists(this DirectoryInfo @this)
        {
            return Directory.CreateDirectory(@this.FullName);
        }

        public static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo @this)
        {
            return Directory.EnumerateDirectories(@this.FullName).Select(x => new DirectoryInfo(x));
        }

        public static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo @this, string searchPattern)
        {
            return Directory.EnumerateDirectories(@this.FullName, searchPattern).Select(x => new DirectoryInfo(x));
        }

        public static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo @this, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateDirectories(@this.FullName, searchPattern, searchOption).Select(x => new DirectoryInfo(x));
        }

        public static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo @this, string[] searchPatterns)
        {
            return searchPatterns.SelectMany(@this.GetDirectories).Distinct();
        }

        public static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo @this, string[] searchPatterns, SearchOption searchOption)
        {
            return searchPatterns.SelectMany(x => @this.GetDirectories(x, searchOption)).Distinct();
        }

        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo @this)
        {
            return Directory.EnumerateFiles(@this.FullName).Select(x => new FileInfo(x));
        }

        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo @this, string searchPattern)
        {
            return Directory.EnumerateFiles(@this.FullName, searchPattern).Select(x => new FileInfo(x));
        }

        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo @this, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateFiles(@this.FullName, searchPattern, searchOption).Select(x => new FileInfo(x));
        }

        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo @this, string[] searchPatterns)
        {
            return searchPatterns.SelectMany(@this.GetFiles).Distinct();
        }

        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo @this, string[] searchPatterns, SearchOption searchOption)
        {
            return searchPatterns.SelectMany(x => @this.GetFiles(x, searchOption)).Distinct();
        }

        public static IEnumerable<string> EnumerateFileSystemEntries(this DirectoryInfo @this)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName);
        }

        public static IEnumerable<string> EnumerateFileSystemEntries(this DirectoryInfo @this, string searchPattern)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName, searchPattern);
        }

        public static IEnumerable<string> EnumerateFileSystemEntries(this DirectoryInfo @this, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName, searchPattern, searchOption);
        }

        public static IEnumerable<string> EnumerateFileSystemEntries(this DirectoryInfo @this, string[] searchPatterns)
        {
            return searchPatterns.SelectMany(x => Directory.EnumerateFileSystemEntries(@this.FullName, x)).Distinct();
        }

        public static IEnumerable<string> EnumerateFileSystemEntries(this DirectoryInfo @this, string[] searchPatterns, SearchOption searchOption)
        {
            return searchPatterns.SelectMany(x => Directory.EnumerateFileSystemEntries(@this.FullName, x, searchOption)).Distinct();
        }

        public static DirectoryInfo[] GetDirectories(this DirectoryInfo @this, string[] searchPatterns)
        {
            return searchPatterns.SelectMany(@this.GetDirectories).Distinct().ToArray();
        }

        public static DirectoryInfo[] GetDirectories(this DirectoryInfo @this, string[] searchPatterns, SearchOption searchOption)
        {
            return searchPatterns.SelectMany(x => @this.GetDirectories(x, searchOption)).Distinct().ToArray();
        }

        public static DirectoryInfo[] GetDirectoriesWhere(this DirectoryInfo @this, Func<DirectoryInfo, bool> predicate)
        {
            return Directory.EnumerateDirectories(@this.FullName).Select(x => new DirectoryInfo(x)).Where(predicate).ToArray();
        }

        public static DirectoryInfo[] GetDirectoriesWhere(this DirectoryInfo @this, string searchPattern, Func<DirectoryInfo, bool> predicate)
        {
            return Directory.EnumerateDirectories(@this.FullName, searchPattern).Select(x => new DirectoryInfo(x)).Where(predicate).ToArray();
        }

        public static DirectoryInfo[] GetDirectoriesWhere(this DirectoryInfo @this, string searchPattern, SearchOption searchOption, Func<DirectoryInfo, bool> predicate)
        {
            return Directory.EnumerateDirectories(@this.FullName, searchPattern, searchOption).Select(x => new DirectoryInfo(x)).Where(predicate).ToArray();
        }

        public static DirectoryInfo[] GetDirectoriesWhere(this DirectoryInfo @this, string[] searchPatterns, Func<DirectoryInfo, bool> predicate)
        {
            return searchPatterns.SelectMany(@this.GetDirectories).Distinct().Where(predicate).ToArray();
        }

        public static DirectoryInfo[] GetDirectoriesWhere(this DirectoryInfo @this, string[] searchPatterns, SearchOption searchOption, Func<DirectoryInfo, bool> predicate)
        {
            return searchPatterns.SelectMany(x => @this.GetDirectories(x, searchOption)).Distinct().Where(predicate).ToArray();
        }

        public static FileInfo[] GetFiles(this DirectoryInfo @this, string[] searchPatterns)
        {
            return searchPatterns.SelectMany(@this.GetFiles).Distinct().ToArray();
        }

        public static FileInfo[] GetFiles(this DirectoryInfo @this, string[] searchPatterns, SearchOption searchOption)
        {
            return searchPatterns.SelectMany(x => @this.GetFiles(x, searchOption)).Distinct().ToArray();
        }

        public static FileInfo[] GetFilesWhere(this DirectoryInfo @this, Func<FileInfo, bool> predicate)
        {
            return Directory.EnumerateFiles(@this.FullName).Select(x => new FileInfo(x)).Where(predicate).ToArray();
        }

        public static FileInfo[] GetFilesWhere(this DirectoryInfo @this, string searchPattern, Func<FileInfo, bool> predicate)
        {
            return Directory.EnumerateFiles(@this.FullName, searchPattern).Select(x => new FileInfo(x)).Where(predicate).ToArray();
        }

        public static FileInfo[] GetFilesWhere(this DirectoryInfo @this, string searchPattern, SearchOption searchOption, Func<FileInfo, bool> predicate)
        {
            return Directory.EnumerateFiles(@this.FullName, searchPattern, searchOption).Select(x => new FileInfo(x)).Where(predicate).ToArray();
        }

        public static FileInfo[] GetFilesWhere(this DirectoryInfo @this, string[] searchPatterns, Func<FileInfo, bool> predicate)
        {
            return searchPatterns.SelectMany(@this.GetFiles).Distinct().Where(predicate).ToArray();
        }

        public static FileInfo[] GetFilesWhere(this DirectoryInfo @this, string[] searchPatterns, SearchOption searchOption, Func<FileInfo, bool> predicate)
        {
            return searchPatterns.SelectMany(x => @this.GetFiles(x, searchOption)).Distinct().Where(predicate).ToArray();
        }

        public static string[] GetFileSystemEntries(this DirectoryInfo @this)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName).ToArray();
        }

        public static string[] GetFileSystemEntries(this DirectoryInfo @this, string searchPattern)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName, searchPattern).ToArray();
        }

        public static string[] GetFileSystemEntries(this DirectoryInfo @this, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName, searchPattern, searchOption).ToArray();
        }

        public static string[] GetFileSystemEntries(this DirectoryInfo @this, string[] searchPatterns)
        {
            return searchPatterns.SelectMany(x => Directory.EnumerateFileSystemEntries(@this.FullName, x)).Distinct().ToArray();
        }

        public static string[] GetFileSystemEntries(this DirectoryInfo @this, string[] searchPatterns, SearchOption searchOption)
        {
            return searchPatterns.SelectMany(x => Directory.EnumerateFileSystemEntries(@this.FullName, x, searchOption)).Distinct().ToArray();
        }

        public static string[] GetFileSystemEntriesWhere(this DirectoryInfo @this, Func<string, bool> predicate)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName).Where(predicate).ToArray();
        }

        public static string[] GetFileSystemEntriesWhere(this DirectoryInfo @this, string searchPattern, Func<string, bool> predicate)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName, searchPattern).Where(predicate).ToArray();
        }

        public static string[] GetFileSystemEntriesWhere(this DirectoryInfo @this, string searchPattern, SearchOption searchOption, Func<string, bool> predicate)
        {
            return Directory.EnumerateFileSystemEntries(@this.FullName, searchPattern, searchOption).Where(predicate).ToArray();
        }

        public static string[] GetFileSystemEntriesWhere(this DirectoryInfo @this, string[] searchPatterns, Func<string, bool> predicate)
        {
            return searchPatterns.SelectMany(x => Directory.EnumerateFileSystemEntries(@this.FullName, x)).Distinct().Where(predicate).ToArray();
        }

        public static string[] GetFileSystemEntriesWhere(this DirectoryInfo @this, string[] searchPatterns, SearchOption searchOption, Func<string, bool> predicate)
        {
            return searchPatterns.SelectMany(x => Directory.EnumerateFileSystemEntries(@this.FullName, x, searchOption)).Distinct().Where(predicate).ToArray();
        }

        public static long GetSize(this DirectoryInfo @this)
        {
            return @this.GetFiles("*.*", SearchOption.AllDirectories).Sum(x => x.Length);
        }

        public static bool IsHidden(this DirectoryInfo directoryInfo)
        {
            return !directoryInfo.IsNotHidden();
        }

        public static bool IsNotHidden(this DirectoryInfo directoryInfo)
        {
            return (directoryInfo.Attributes & FileAttributes.Hidden) == 0;
        }

        public static string PathCombine(this DirectoryInfo @this, params string[] paths)
        {
            List<string> list = paths.ToList();
            list.Insert(0, @this.FullName);
            return Path.Combine(list.ToArray());
        }

        public static DirectoryInfo PathCombineDirectory(this DirectoryInfo @this, params string[] paths)
        {
            List<string> list = paths.ToList();
            list.Insert(0, @this.FullName);
            return new DirectoryInfo(Path.Combine(list.ToArray()));
        }

        public static FileInfo PathCombineFile(this DirectoryInfo @this, params string[] paths)
        {
            List<string> list = paths.ToList();
            list.Insert(0, @this.FullName);
            return new FileInfo(Path.Combine(list.ToArray()));
        }
    }
}