using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfBookletDemo
{
    public static class BookletGenerator
    {
        /// <summary>
        /// Generates a booklet PDF using only the left (even‑numbered) pages from the first half of the source PDF.
        /// Returns true if the operation succeeds.
        /// </summary>
        /// <param name="inputFile">Path to the source PDF file.</param>
        /// <param name="outputFile">Path where the booklet PDF will be saved.</param>
        /// <returns>True on success, false otherwise.</returns>
        public static bool CreateLeftHalfBooklet(string inputFile, string outputFile)
        {
            // Validate input file existence
            if (!File.Exists(inputFile))
                throw new FileNotFoundException($"Source file not found: {inputFile}");

            // Load the source PDF to determine page count (using Aspose.Pdf.Document)
            using (Document srcDoc = new Document(inputFile))
            {
                int totalPages = srcDoc.Pages.Count;               // 1‑based page count
                int halfPages = totalPages / 2;                    // First half (floor if odd)

                // Collect even (left) and odd (right) page numbers from the first half
                List<int> leftPagesList = new List<int>();
                List<int> rightPagesList = new List<int>();

                for (int i = 1; i <= halfPages; i++)
                {
                    if (i % 2 == 0)      // Even page numbers are left pages
                        leftPagesList.Add(i);
                    else                 // Odd page numbers are right pages
                        rightPagesList.Add(i);
                }

                int[] leftPages = leftPagesList.ToArray();
                int[] rightPages = rightPagesList.ToArray();

                // Use PdfFileEditor (Facades API) to create the customized booklet
                PdfFileEditor editor = new PdfFileEditor();
                bool success = editor.MakeBooklet(inputFile, outputFile, leftPages, rightPages);
                return success;
            }
        }
    }

    // Entry point required for a console application
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Simple argument handling – if arguments are missing, show usage.
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: AsposePdfBookletDemo <input-pdf> <output-pdf>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                bool result = BookletGenerator.CreateLeftHalfBooklet(inputPath, outputPath);
                Console.WriteLine(result ? "Booklet created successfully." : "Booklet creation failed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
