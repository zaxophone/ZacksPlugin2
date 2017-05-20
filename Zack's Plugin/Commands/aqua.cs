﻿using PluginAPI;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace AquaConsole.Commands
{
    class aqua : ICommand
    {
        public string Command
        {
            get
            {
                return "aqua";
            }
        }

        public string HelpText
        {
            get
            {
                return "Runs a file in AquaConsole native language";
            }
        }

        public void CommandMethod(string[] p)
        {
            string cheese = (string.Join("", p));
            if (cheese == "help")
            {
                Utility.NotifyWriteLine("Usage: cd (directory) then aqua (filename with ext)");
            }
            else
            {
                if (Utility.FileOrDirectoryExists(cheese))
                {
                    List<List<string>> groups = new List<List<string>>();
                    foreach (var line in File.ReadAllLines(cheese))
                    {
                        string AquaConsoleExe = (Assembly.GetExecutingAssembly().Location + "\\AquaConsole.exe");
                        string[] readText = File.ReadAllLines(cheese);

                        Process ac = new Process();
                        ac.StartInfo.FileName = AquaConsoleExe;
                        ac.StartInfo.UseShellExecute = false;
                        ac.StartInfo.RedirectStandardInput = true;

                        ac.Start();

                        StreamWriter acsw = ac.StandardInput;
                        acsw.WriteLine(line);
                    }
                }
                else
                    Utility.ErrorWriteLine("Warning- File is nonexistent or you have insufficient permissions to access it or your file system is corrupt or there's a system I/O error");
            }
        }
    }
}