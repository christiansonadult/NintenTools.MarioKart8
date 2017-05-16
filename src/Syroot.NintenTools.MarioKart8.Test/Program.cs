using System;
using System.Diagnostics;
using System.IO;
using Syroot.NintenTools.MarioKart8.Collisions;

namespace Syroot.NintenTools.MarioKart8.Test
{
    internal class Program
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private static Stopwatch _stopwatch = new Stopwatch();
        private static string[] _searchPaths = new string[]
        {
            @"D:\Archive\Wii U\Roms\MK8"
        };

        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        private static void Main(string[] args)
        {
            KclFile kcl = new KclFile(@"D:\Archive\Wii U\Roms\MK8\content\course\Gu_FirstCircuit\course.kcl");
            kcl.Save(@"D:\Archive\Wii U\Roms\MK8\content\course\Gu_FirstCircuit\course.kcl.new");

            //LoadFiles<KclFile>("course.kcl");
        }
        
        private static void LoadFiles<T>(string searchPattern) where T : ILoadableFile, new()
        {
            foreach (string searchPath in _searchPaths)
            {
                foreach (string fileName in Directory.GetFiles(searchPath, searchPattern, SearchOption.AllDirectories))
                {
                    Console.Write($"Loading {fileName}...");
                    T file = new T();
                    _stopwatch.Restart();
                    file.Load(fileName);
                    _stopwatch.Stop();
                    Console.WriteLine($" {_stopwatch.ElapsedMilliseconds}ms");
                }
            }
        }
    }
}
