using Microsoft.Win32.TaskScheduler;
using System;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using Task = Microsoft.Win32.TaskScheduler.Task;

namespace Simple_Temp_Cleaner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string arg = args[0];

                if (arg == "--install" || arg == "-i")
                {
                    Install();
                }
                else if (arg == "--uninstall" || arg == "-u")
                {
                    Uninstall();
                }
                else
                {
                    Console.WriteLine("Invalid argument. Valid arguments: --install, -i, --uninstall, -u");
                }

                return;
            }

            RecursiveDelete(Path.GetTempPath()); // %temp%
            RecursiveDelete(@"C:\Windows\Temp\"); // temp
            RecursiveDelete(Environment.GetFolderPath(Environment.SpecialFolder.Recent)); // recent
        }

        static void RecursiveDelete(string path, bool deleteSelf = false)
        {
            string[] files = Directory.GetFiles(path);
            string[] folders = Directory.GetDirectories(path);

            foreach (string file in files)
            {
                try
                {
                    File.Delete(file);
                    Console.WriteLine($"[+] {file}");
                }
                catch (Exception)
                {
                    Console.WriteLine($"[-] {file}");
                }
            }

            foreach (string folder in folders)
            {
                RecursiveDelete(folder, true);
            }

            if (deleteSelf)
            {
                try
                {
                    Directory.Delete(path, true);
                    Console.WriteLine($"[+] {path}");
                }
                catch (Exception)
                {
                    Console.WriteLine($"[-] {path}");
                }
            }

        }

        private static string TaskName = "SimpleTempCleaner";

        static void Install()
        {
            using (TaskService ts = new TaskService())
            {
                Task task = ts.GetTask(TaskName);

                if (task != null)
                {
                    Console.WriteLine("Already installed.");
                }
                else
                {
                    TaskDefinition td = ts.NewTask();
                    td.Settings.Hidden = true;
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Principal.LogonType = TaskLogonType.S4U;
                    td.RegistrationInfo.Description = "Deletes temporary files.";
                    td.Triggers.Add(new LogonTrigger
                    {
                        UserId = WindowsIdentity.GetCurrent().User.ToString(),
                        Repetition = new RepetitionPattern(TimeSpan.FromMilliseconds(1000 * 60 * 10), TimeSpan.Zero)
                    });
                    td.Actions.Add(new ExecAction(Assembly.GetExecutingAssembly().Location));
                    ts.RootFolder.RegisterTaskDefinition(TaskName, td);
                    Console.WriteLine("Installed.");
                }
            }
        }

        static void Uninstall()
        {

            using (TaskService ts = new TaskService())
            {
                Task task = ts.GetTask(TaskName);

                if (task == null)
                {
                    Console.WriteLine("Already not installed.");
                }
                else
                {
                    ts.RootFolder.DeleteTask(TaskName);
                    Console.WriteLine("Uninstalled.");
                }
            }
        }
    }
}
