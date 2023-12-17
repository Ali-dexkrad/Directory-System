using System;
using System.IO;

public class Directory_System
{
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

    public static void Main(string[] args)
    {
        GetMenu();
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
                string statusD = InsertDirectory(rootPath+pathD);
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
                string status = InsertFile(rootPath+path);
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
                
            case "s":
                Console.Clear();
                PrintTree(rootPath, "", true);
                Console.Write("\n\n\nEnter The File Or Directory Path>");
                string pathS;
                pathS = Console.ReadLine();
                string statusS = Search(rootPath + pathS);
                if (statusS == "Found")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(statusS);
                    Console.ResetColor(); Console.ReadKey();
                    Console.Clear(); PrintTree(rootPath, "", true);

                    goto hare;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(statusS);
                    Console.ResetColor(); Console.ReadKey();
                    Console.Clear();
                    PrintTree(rootPath, "", true);

                    goto hare;
                }
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The command is invalid");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.ReadKey();
                goto hare;
                break;
        }
        Console.ReadKey();
    }

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
| s : Search Directory Or File      |
|___________________________________|
 ");


    }

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
    private static string Search(string path)
    {
        string status = "";
        try
        {
            if (Directory.Exists(path))
            {
                status = "Found";
            }
            else if (File.Exists(path))
            {
                status = "Found";
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
}