using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSysProg
{
    public enum FileType
    {
        File,
        Directory
    }

    public abstract class FileItem
    {

        public FileType Type { get; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string FullName { get; set; }

        public FileItem(FileType type, string name, string path)
        {
            this.Type = type;
            this.Name = name;
            this.Path = path;
        }
        public abstract void Display(int depth);
    }
    public class File : FileItem
    {
        public File(string name, string path, string ext) : base(FileType.File, name, path)
        {
            this.Ext = ext;
        }
        public string Ext { get; set; }
        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + $"{Name}.{Ext}");
        }

    }
    public class Directory : FileItem
    {
        private List<FileItem> Content = new List<FileItem>();
        public Directory(string name, string path) : base(FileType.Directory, name, path)
        {

        }
        public void Add(FileItem item)
        {
            Content.Add(item);
        }
        public Directory? FindDirectoryByName(string name)
        {
           


            foreach (var item in Content)
            {


                if (item.Type == FileType.Directory)
                {
                    Directory d = (Directory)item;


                    if (d.Name == name)
                    {
                        return d;
                    }
                    return d.FindDirectoryByName(name);
                }


            }

            return null;
        }
        public bool HasFile(string name) { return Content.Any(x => x.Name == name); }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + Name);
            foreach (FileItem item in Content)
            {
                item.Display(depth + 2);
            }
        }
    }
    public class FileSystem
    {
        private Dictionary<string, string> files = new Dictionary<string, string>();
        private Dictionary<string, FileType> AllPathes = new Dictionary<string, FileType>();
        private Directory RootPath = new Directory("root", "/");


        public void CreateFile(string path)
        {

            string name = Path.GetFileName(path);
            string ext = Path.GetExtension(path);
            string[] dirArr = path.Split("/");
            string dir = dirArr.Length > 1 ? dirArr[dirArr.Length -2] : "root";
            Console.WriteLine("Name: " +name);
            Console.WriteLine("dir: " + dir);
            Directory? directory = dir == "root" ? RootPath : RootPath.FindDirectoryByName(dir);

            if (directory == null)
            {
                System.Console.WriteLine("File can not create, because directory there is not in path");
                return;
            }
            if (directory.HasFile(name))
            {
                System.Console.WriteLine($"File with name {name} exist");
                return;
            }
            File file = new File(path, name, ext);

            directory.Add(file);
            System.Console.WriteLine($"File:\nName: {name}; Ext: {ext}; Path: {path} - created");
        }

        public void CreateDirectory(string path)
        {

            string name = Path.GetFileName(path);


            Directory directory = RootPath.FindDirectoryByName(name) ?? RootPath;

            if (directory == null)
            {
                System.Console.WriteLine("File can not create, because directory there is not in path");
                return;
            }
            if (directory.HasFile(name))
            {
                System.Console.WriteLine($"File with name {name} exist");
                return;
            }
            Directory file = new Directory(path, name);

            directory.Add(file);
            System.Console.WriteLine($"File:\nName: {name}; FullPath: {path} - created");
        }

        public void WriteFile(string path, string data)
        {
            if (files.ContainsKey(path))
            {
                files[path] = data;
            }
        }

        public string? ReadFile(string path)
        {
            return files.ContainsKey(path) ? files[path] : null;
        }

        public void DeleteFile(string path)
        {
            if (files.ContainsKey(path))
            {
                files.Remove(path);
            }
        }

        public List<string> GetFiles()
        {
            return files.Keys.ToList();
        }
        public void ShowFiles()
        {
            RootPath.Display(1);
        }
    }
}
