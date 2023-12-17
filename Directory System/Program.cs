using System;
using System.IO;

public class TreeView
{
    private static void PrintTree(string path, string indent, bool isLast)
    {
        var directoryInfo = new DirectoryInfo(path);
        var files = directoryInfo.GetFiles();
        var directories = directoryInfo.GetDirectories();

        Console.WriteLine(indent + (isLast ? "└─" : "├─") + directoryInfo.Name);

        indent += isLast ? "   " : "│  ";

        for (int i = 0; i < directories.Length; i++)
        {
            PrintTree(directories[i].FullName, indent, i == directories.Length - 1);
        }

        for (int i = 0; i < files.Length; i++)
        {
            Console.WriteLine(indent + (i == files.Length - 1 ? "└─" : "├─") + files[i].Name);
        }
    }

    public static void Main(string[] args)
    {
        string executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        string executableDirectory = System.IO.Path.GetDirectoryName(executablePath);
        string rootPath = executableDirectory+ @"\Root\";

        Console.WriteLine(rootPath);
        PrintTree(rootPath, "", true);

        Console.ReadKey();
    }
}