using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace BookletApp
{
    public static class BookletGenerator
    {
        /// <summary>
        /// Generates a booklet PDF that contains only the left (even‑numbered) pages
        /// from the first half of the source document.
        /// </summary>
        /// <param name="sourcePdfPath">Path to the source PDF.</param>
        /// <param name="outputPdfPath">Path where the booklet PDF will be saved.</param>
        public static void GenerateLeftPageBooklet(string sourcePdfPath, string outputPdfPath)
        {
            if (!File.Exists(sourcePdfPath))
                throw new FileNotFoundException($"Source file not found: {sourcePdfPath}");

            // Load the source PDF to determine page count.
            using (Document srcDoc = new Document(sourcePdfPath))
            {
                int totalPages = srcDoc.Pages.Count;
                int halfPages = totalPages / 2; // integer division – first half of the document

                // Create a temporary PDF that will hold the selected left pages.
                using (Document tempDoc = new Document())
                {
                    // Left pages are even‑numbered (2,4,6,…). Page indexing is 1‑based.
                    for (int pageNum = 2; pageNum <= halfPages; pageNum += 2)
                    {
                        // Add the page from the source document to the temporary document.
                        tempDoc.Pages.Add(srcDoc.Pages[pageNum]);
                    }

                    // Save the temporary PDF to a uniquely named file.
                    string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
                    tempDoc.Save(tempPath);

                    try
                    {
                        // Use the Facades API to create a booklet from the temporary PDF.
                        // PdfFileEditor does NOT implement IDisposable, so we do NOT wrap it in a using block.
                        PdfFileEditor editor = new PdfFileEditor();

                        // Use the overload that does not require a PageSize argument.
                        editor.MakeBooklet(tempPath, outputPdfPath);
                    }
                    finally
                    {
                        // Clean up the temporary file.
                        if (File.Exists(tempPath))
                            File.Delete(tempPath);
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expect exactly two arguments: source PDF path and output PDF path.
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: BookletApp <sourcePdfPath> <outputPdfPath>");
                return;
            }

            try
            {
                BookletGenerator.GenerateLeftPageBooklet(args[0], args[1]);
                Console.WriteLine("Booklet created successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}