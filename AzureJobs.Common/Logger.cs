﻿using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AzureJobs.Common
{
    public class Logger
    {
        private string _path;
        private static object _syncRoot = new object();

        public Logger(string path)
        {
            _path = path;
}

        public bool Exist()
        {
            return File.Exists(GetLogFilePath());
        }

        public void Write(params string[] messages)
        {
            Trace.WriteLine(string.Join(", ", messages));
            Console.WriteLine(string.Join(", ", messages));

            string file = GetLogFilePath();

            if (!File.Exists(file))
                File.WriteAllLines(file, new[] { "Log file created" });

            lock (_syncRoot)
            {
                File.AppendAllLines(file, messages);
            }
        }

        private string GetLogFilePath()
        {
            var ass = Assembly.GetEntryAssembly();
            string name = ass.ManifestModule.Name;
            return Path.Combine(_path, name + ".txt");
        }
    }
}