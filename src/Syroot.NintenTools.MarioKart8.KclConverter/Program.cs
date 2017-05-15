using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.KclConverter
{
    /// <summary>
    /// Represents the main class of the application containing the program entry point.
    /// </summary>
    internal class Program
    {
        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        private static void Main(string[] args)
        {
            string inputFile = args[0];
            string outputFile = Path.ChangeExtension(inputFile, "mk8.kcl");

            using (BinaryDataReader reader = new BinaryDataReader(
                new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read)))
            using (BinaryDataWriter writer = new BinaryDataWriter(
                new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                ConvertFile(reader, writer);
            }
        }

        private static void ConvertFile(BinaryDataReader reader, BinaryDataWriter writer)
        {

        }
    }
}
