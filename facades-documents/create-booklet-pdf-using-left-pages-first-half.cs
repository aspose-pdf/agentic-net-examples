using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace BookletDemo
{
    public static class BookletGenerator
    {
        /// <summary>
        /// Creates a booklet PDF using the left pages from the first half of the source PDF.
        /// The left pages are taken as even-numbered pages within the first half,
        /// while the right pages are the corresponding odd-numbered pages.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF file.</param>
        /// <param name="outputPdfPath">Path where the booklet PDF will be saved.</param>
        public static void CreateBooklet(string inputPdfPath, string outputPdfPath)
        {
            if (string.IsNullOrWhiteSpace(inputPdfPath))
                throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

            if (string.IsNullOrWhiteSpace(outputPdfPath))
                throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException("Input PDF file not found.", inputPdfPath);

            // Determine page count using the core Document API (lifecycle rule: use using for disposal)
            int totalPages;
            using (Document doc = new Document(inputPdfPath))
            {
                totalPages = doc.Pages.Count;
            }

            if (totalPages < 2)
                throw new InvalidOperationException("The source PDF must contain at least two pages to create a booklet.");

            // Calculate the first half (integer division)
            int half = totalPages / 2;

            // Build left (even) and right (odd) page arrays from the first half
            List<int> leftPages = new List<int>();
            List<int> rightPages = new List<int>();

            for (int i = 1; i <= half; i++)
            {
                if (i % 2 == 0)
                    leftPages.Add(i);   // even page -> left side
                else
                    rightPages.Add(i);  // odd page -> right side
            }

            // Use PdfFileEditor facade to create the booklet with custom page ordering
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.MakeBooklet(
                inputPdfPath,
                outputPdfPath,
                leftPages.ToArray(),
                rightPages.ToArray());

            if (!success)
                throw new InvalidOperationException("Failed to create booklet using PdfFileEditor.");
        }
    }

    // Entry point required for a console‑style build. It simply demonstrates the API usage.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Expect two arguments: input PDF path and output PDF path.
            // If not supplied, display a short usage message and exit.
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: BookletDemo <input-pdf> <output-pdf>");
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