﻿using PluginAPI;
using System;
using System.IO;
using System.Net;
//necessary for compression
using Ionic.Zip;
//
using System.Threading;
using System.Reflection;

class update : ICommand
{
    public string Command
    {
        get
        {
            return "update";
        }
    }

    public string HelpText
    {
        get
        {
            return "Updates to the specified version";
        }
    }

    public void CommandMethod(string[] p)
    {
        string cheese = (string.Join("", p));

        string release = cheese;
        string remoteUri = ("https://github.com/greyblockgames/AquaConsole/releases/download/" + release + "/AquaConsole.zip");
        string fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string parent = Directory.GetParent(fileName).FullName;
        string zipname = (fileName + "\\AquaConsole.zip");
        string releaseFolder = (parent + "\\" + release);
        string pluginsFolder = fileName;
        string newPluginsFolder = (releaseFolder + "\\plugins");

        if (release == ("help"))
        {
            Console.WriteLine("Usage: update (version)");
        }
        else
        {
            using (var client = new WebClient())
            {
                if (!Utility.FileOrDirectoryExists(fileName))
                {
                    Console.WriteLine("AquaConsole release already found!");
                }
                else
                {
                    WebClient temporaryw = new WebClient();
                    temporaryw.DownloadFile(remoteUri, zipname);
                    using (ZipFile zip1 = ZipFile.Read(zipname))
                    {
                        zip1.ExtractAll(releaseFolder);
                    }
                    File.Delete(zipname);
                    Console.WriteLine("Added version " + release + " in new folder");
                    Directory.CreateDirectory(newPluginsFolder);

                    string zippi = (newPluginsFolder + "\\temp.zip");
                    using (ZipFile zip2 = new ZipFile())
                    {
                        zip2.AddDirectory(pluginsFolder, "");
                        zip2.Save(zippi);
                    }
                    using (ZipFile zip3 = ZipFile.Read(zippi))
                    {
                        zip3.ExtractAll(newPluginsFolder);
                    }
                    Console.WriteLine("Successfully copied over plugins");
                    Thread.Sleep(8000);
                    File.Delete(zippi);
                }
            }
        }
    }
}
