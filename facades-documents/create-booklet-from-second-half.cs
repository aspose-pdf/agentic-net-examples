using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public class BookletGenerator
    {
        /// <summary>
        /// Generates a booklet PDF that contains only the right (odd‑numbered) pages
        /// from the second half of the source PDF.
        /// </summary>
        /// <param name="sourcePdfPath">Path to the source PDF.</param>
        /// <param name="outputPdfPath">Path where the booklet PDF will be saved.</param>
        public static void GenerateBooklet(string sourcePdfPath, string outputPdfPath)
        {
            if (!File.Exists(sourcePdfPath))
                throw new FileNotFoundException($"Source file not found: {sourcePdfPath}");

            // Load the source document to obtain the total page count.
            // Document must be wrapped in a using block for deterministic disposal.
            using (Document srcDoc = new Document(sourcePdfPath))
            {
                int totalPages = srcDoc.Pages.Count;

                // Determine the first page of the second half.
                // For even page counts the second half starts at (totalPages / 2) + 1.
                // For odd page counts the integer division already floors the value,
                // so the same formula works.
                int secondHalfStart = (totalPages / 2) + 1;

                // Collect odd‑numbered (right) pages from the second half.
                List<int> rightPages = new List<int>();
                for (int i = secondHalfStart; i <= totalPages; i++)
                {
                    if (i % 2 == 1) // odd page number = right page
                        rightPages.Add(i);
                }

                // If no pages match the criteria, create an empty PDF.
                if (rightPages.Count == 0)
                {
                    using (Document emptyDoc = new Document())
                    {
                        emptyDoc.Save(outputPdfPath);
                    }
                    return;
                }

                // Use PdfFileEditor (Facades API) to extract the selected pages.
                // PdfFileEditor does NOT implement IDisposable, so no using block is needed.
                PdfFileEditor editor = new PdfFileEditor();
                // NOTE: In some Aspose.PDF versions the overload order is
                // Extract(sourcePath, pageNumbers, outputPath).
                // Swapping the last two arguments ensures compatibility.
                editor.Extract(sourcePdfPath, rightPages.ToArray(), outputPdfPath);
            }
        }
    }

    public class Program
    {
        /// <summary>
        /// Entry point required for a console application.
        /// Expects two arguments: <c>sourcePdfPath</c> and <c>outputPdfPath</c>.
        /// </summary>
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: AsposePdfApi <sourcePdfPath> <outputPdfPath>");
                return;
            }

            string sourcePath = args[0];
            string outputPath = args[1];

            try
            {
                BookletGenerator.GenerateBooklet(sourcePath, outputPath);
                Console.WriteLine($"Booklet successfully created at '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error generating booklet: {ex.Message}");
            }
        }
    }
}
