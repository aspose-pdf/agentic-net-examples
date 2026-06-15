using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class BookletGenerator
    {
        /// <summary>
        /// Creates a booklet PDF using the left (even) pages from the first half of the source PDF.
        /// The right (odd) pages are taken from the same first half.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF.</param>
        /// <param name="outputPdfPath">Path where the booklet PDF will be saved.</param>
        public static void CreateBooklet(string inputPdfPath, string outputPdfPath)
        {
            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException($"Source PDF not found: {inputPdfPath}");

            // Determine page count using the core Document API (wrapped in a using block for proper disposal)
            int totalPages;
            using (Document srcDoc = new Document(inputPdfPath))
            {
                totalPages = srcDoc.Pages.Count;
            }

            // Calculate the first half of the document (integer division)
            int halfCount = totalPages / 2;

            // Build left (even) and right (odd) page arrays from the first half
            List<int> leftPagesList = new List<int>();
            List<int> rightPagesList = new List<int>();

            for (int i = 1; i <= halfCount; i++)
            {
                if (i % 2 == 0)
                    leftPagesList.Add(i);   // even page -> left side
                else
                    rightPagesList.Add(i);  // odd page -> right side
            }

            int[] leftPages = leftPagesList.ToArray();
            int[] rightPages = rightPagesList.ToArray();

            // Use PdfFileEditor to create the customized booklet
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.MakeBooklet(inputPdfPath, outputPdfPath, leftPages, rightPages);

            if (!success)
                throw new InvalidOperationException("Booklet creation failed.");
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Simple command‑line interface: first argument = input PDF, second argument = output PDF
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: AsposePdfApi <inputPdfPath> <outputPdfPath>");
                return;
            }

            try
            {
                BookletGenerator.CreateBooklet(args[0], args[1]);
                Console.WriteLine("Booklet created successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}