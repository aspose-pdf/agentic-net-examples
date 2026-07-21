using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace BookletApp
{
    public static class BookletGenerator
    {
        /// <summary>
        /// Generates a booklet PDF using the left (even) pages from the first half of the source PDF.
        /// The method creates matching right (odd) pages from the same half and invokes
        /// PdfFileEditor.MakeBooklet to produce the booklet.
        /// </summary>
        /// <param name="inputPdfPath">Full path to the source PDF.</param>
        /// <param name="outputPdfPath">Full path where the booklet PDF will be saved.</param>
        /// <returns>True if the booklet was created successfully; otherwise false.</returns>
        public static bool CreateBooklet(string inputPdfPath, string outputPdfPath)
        {
            if (string.IsNullOrEmpty(inputPdfPath))
                throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

            if (string.IsNullOrEmpty(outputPdfPath))
                throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

            // Load the source document to determine the total number of pages.
            // Document implements IDisposable, so we wrap it in a using block.
            using (Document srcDoc = new Document(inputPdfPath))
            {
                int totalPages = srcDoc.Pages.Count;

                // Determine the first half of the document (integer division).
                int halfPages = totalPages / 2;

                // Collect left (even) and right (odd) page numbers from the first half.
                List<int> leftPages = new List<int>();
                List<int> rightPages = new List<int>();

                for (int i = 1; i <= halfPages; i++)
                {
                    if (i % 2 == 0)
                        leftPages.Add(i);   // even page -> left side of booklet
                    else
                        rightPages.Add(i);  // odd page -> right side of booklet
                }

                // Use PdfFileEditor to create the booklet with the calculated page arrays.
                PdfFileEditor editor = new PdfFileEditor();
                bool result = editor.MakeBooklet(
                    inputPdfPath,
                    outputPdfPath,
                    leftPages.ToArray(),
                    rightPages.ToArray());

                return result;
            }
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point required for a console executable. It forwards arguments to the
        /// BookletGenerator.CreateBooklet method and reports the outcome.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: BookletApp <inputPdfPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                bool success = BookletGenerator.CreateBooklet(inputPath, outputPath);
                Console.WriteLine(success ? "Booklet created successfully." : "Failed to create booklet.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
