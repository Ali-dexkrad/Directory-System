using System;
using System.IO;

public class Directory_System
{
    //  بارگذاری درخت فایل و پوشه ها
    private static void PrintTree(string path, string indent, bool isLast)
    {   
        Console.ForegroundColor = ConsoleColor.DarkGreen;

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

    //تابع اصلی
    public static void Main(string[] args)
    {
        GetMenu();

        // پیدا کردن مسیر پوشه روت
        string executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        string executableDirectory = System.IO.Path.GetDirectoryName(executablePath);
        string rootPath = executableDirectory + @"\Root\";

    hare:
        string commend;
        Console.Write("\n\n\n\n {0}>", "Enter the command");
        commend = Console.ReadLine();
        switch (commend)
        {
            case "tree":
                Console.Clear();
                PrintTree(rootPath, "", true);
                goto hare;
            case "menu":
                GetMenu();
                goto hare;
            case "insd":
                Console.Clear();
                PrintTree(rootPath, "", true);
                Console.Write("\n\n\nEnter Directory Path >");
                string pathD;
                pathD = Console.ReadLine();
                string statusD = InsertDirectory(rootPath + pathD);
                if (statusD == "done successfully")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(statusD);
                    Console.ReadKey();
                    Console.ResetColor(); Console.Clear();
                    PrintTree(rootPath, "", true);
                    goto hare;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(statusD);
                    Console.ResetColor(); Console.ReadKey();
                    Console.Clear();
                    PrintTree(rootPath, "", true);
                    goto hare;
                }
            case "insf":
                Console.Clear();
                PrintTree(rootPath, "", true);
                Console.Write("\n\n\nEnter the file path (along with the file format)>");
                string path;
                path = Console.ReadLine();
                string status = InsertFile(rootPath + path);
                if (status == "done successfully")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(status);
                    Console.ResetColor(); Console.ReadKey();
                    Console.Clear(); PrintTree(rootPath, "", true);

                    goto hare;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(status);
                    Console.ResetColor(); Console.ReadKey();
                    Console.Clear(); PrintTree(rootPath, "", true);

                    goto hare;
                }
            case "del":
                Console.Clear();
                PrintTree(rootPath, "", true);
                Console.Write("\n\n\nEnter The File Or Directory Path>");
                string pathdel;
                pathdel = Console.ReadLine();
                string statusdel = Delete(rootPath + pathdel);
                if (statusdel == "done successfully")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(statusdel);
                    Console.ResetColor(); Console.ReadKey();
                    Console.Clear(); PrintTree(rootPath, "", true);

                    goto hare;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(statusdel);
                    Console.ResetColor(); Console.ReadKey();
                    Console.Clear(); PrintTree(rootPath, "", true);

                    goto hare;
                }

            case "sf":
                Console.Clear();
                PrintTree(rootPath, "", true);
                Console.Write("\n\n\nEnter The File Name>");
                string pathS;
                pathS = Console.ReadLine();
                string statusS = SearchFile(pathS, rootPath);

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(statusS);
                Console.ResetColor(); Console.ReadKey();
                Console.Clear(); PrintTree(rootPath, "", true);

                goto hare;
            case "sd":
                Console.Clear();
                PrintTree(rootPath, "", true);
                Console.Write("\n\n\nEnter The Directory Name>");
                string pathSd;
                pathSd = Console.ReadLine();
                string statusSd = SearchDirectory(pathSd, rootPath);

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(statusSd);
                Console.ResetColor(); Console.ReadKey();
                Console.Clear(); PrintTree(rootPath, "", true);

                goto hare;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The command is invalid");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.ReadKey();
                goto hare;
        }
    }

    // منوی برنامه
    private static void GetMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(@" _________________ 
/                 \
\                 /
  Directory System
/                 \
\_________________/");

        Console.WriteLine("\n\n");

        Console.WriteLine(@" ___________________________________
|Commands :                         |
|                                   |
| tree : Load Tree                  |
| menu : Show menu                  |
|                                   |
| insd : Insert Directory           |
| insf : Insert File                |
| del : Delete Directory Of File    |
| sf : Search File                  |
| sd : Search Directory             |
|___________________________________|");


    }

    /// <summary>
    /// افزودن پوشه جدید به محل روت
    /// </summary>
    /// <param name="path">مسیر پوشه</param>
    /// <returns></returns>
    private static string InsertDirectory(string path)
    {
        string status = "";
        try
        {
            if (Directory.Exists(path))
            {
                status = "This folder already exists";
            }
            else
            {
                Directory.CreateDirectory(path);
                status = "done successfully";
            }
        }
        catch (Exception ex)
        {
            status = ex.Message;
        }

        return status;
    }

    /// <summary>
    /// افزودن فایل جدید به محل روت
    /// </summary>
    /// <param name="path">مسیر فایل</param>
    /// <returns></returns>
    private static string InsertFile(string path)
    {
        string status = "";
        try
        {
            if (File.Exists(path))
            {
                status = "This File already exists";
            }
            else
            {
                File.Create(path);
                status = "done successfully";
            }
        }
        catch (Exception ex)
        {
            status = ex.Message;
        }
      
        return status;
    }

    /// <summary>
    /// حذف پوشه یا فایل
    /// </summary>
    /// <param name="path">مسیر پوشه یا فایل را میتوان داد</param>
    /// <returns></returns>
    private static string Delete(string path)
    {
        string status = "";
        try
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path,true);
                status = "done successfully";
            }
            else if (File.Exists(path))
            {
                File.Delete(path);
                status = "done successfully";
            }
            else
            {
                status = "The path is invalid";
            }
        }
        catch (Exception ex)
        {
            status = ex.Message;
        }
        return status;
    }

    /// <summary>
    /// تابع جستجوی فایل
    /// </summary>
    /// <param name="path">فقط نام فایل به همراه فرمت</param>
    /// <param name="treepath">مسیر اصلی روت</param>
    /// <returns></returns>
    private static string SearchFile(string path, string treepath)
    {
        string status = "";
        try
        {
            string[] foundPaths = Directory.GetFiles(treepath, path, SearchOption.AllDirectories);
            if (foundPaths.Length > 0)
            {
                foreach (string path1 in foundPaths)
                {
                    status = "Found paths:" + path1;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Not Found");
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            status = ex.Message;
        }
        return status;
    }

    /// <summary>
    /// تابع جستجوی پوشه
    /// </summary>
    /// <param name="path1">فقط نام پوشه </param>
    /// <param name="treepath">مسیر اصلی روت</param>
    /// <returns></returns>
    private static string SearchDirectory(string path1, string treepath)
    {
        string status = "";
        try
        {
            string folderName = path1;
            string path = treepath;

            string[] directories = Directory.GetDirectories(path, folderName, SearchOption.AllDirectories);

            if (directories.Length > 0)
            {
                Console.WriteLine($"Directory Path : {folderName}");

                foreach (string directory in directories)
                {
                    Console.WriteLine(directory);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Not Found");
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            status = ex.Message;
        }
        return status;
    }
}