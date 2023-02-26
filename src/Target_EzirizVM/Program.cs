using System;
using System.Diagnostics;
using System.IO;

namespace Target_EzirizVM
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var processModule = Process.GetCurrentProcess().MainModule;
            string filepath = processModule?.FileName, filename = Path.GetFileName(filepath);

            Console.WriteLine("usage: nunit-console {0}", filename);
        }
    }
}