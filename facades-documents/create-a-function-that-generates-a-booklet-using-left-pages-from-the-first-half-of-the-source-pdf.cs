using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace BookletApp
{
    public static class BookletGenerator
    {
        /// <summary>
        /// Creates a booklet PDF using the left (even) pages from the first half of the source PDF.
        /// The right (odd) pages from the same range are also supplied because the MakeBooklet
        /// overload requires both left and right page arrays.
        /// </summary>
        /// <param name="inputPath">Full path to the source PDF.</param>
        /// <param name="outputPath">Full path where the booklet PDF will be saved.</param>
        public static void CreateLeftPageBooklet(string inputPath, string outputPath)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                throw new ArgumentException("Input path must be provided.", nameof(inputPath));
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be provided.", nameof(outputPath));

            // Load the source document to determine the total page count.
            // Document implements IDisposable, so wrap it in a using block.
            using (Document srcDoc = new Document(inputPath))
            {
                int totalPages = srcDoc.Pages.Count;               // 1‑based page count
                int halfPages = totalPages / 2;                    // integer division → first half

                var leftPages = new List<int>(); // even page numbers (left side)
                var rightPages = new List<int>(); // odd page numbers (right side)

                // Pages are 1‑based. Collect pages up to halfPages.
                for (int i = 1; i <= halfPages; i++)
                {
                    if (i % 2 == 0)
                        leftPages.Add(i);   // even → left
                    else
                        rightPages.Add(i);  // odd  → right
                }

                // Instantiate the facade (PdfFileEditor does NOT implement IDisposable).
                PdfFileEditor editor = new PdfFileEditor();

                // Use the overload that accepts custom left/right page arrays.
                // This creates a booklet where the specified left pages are paired with the right pages.
                bool success = editor.MakeBooklet(
                    inputPath,
                    outputPath,
                    leftPages.ToArray(),
                    rightPages.ToArray());

                if (!success)
                    throw new InvalidOperationException("Booklet creation failed.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expecting two arguments: input PDF path and output PDF path.
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: BookletApp <inputPdfPath> <outputPdfPath>");
                return;
            }

            try
            {
                BookletGenerator.CreateLeftPageBooklet(args[0], args[1]);
                Console.WriteLine("Booklet created successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
