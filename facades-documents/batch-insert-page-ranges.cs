using System;
using System.IO;
using Aspose.Pdf;               // Core API for creating PDFs
using Aspose.Pdf.Facades;      // Facade API for batch insertion

class Program
{
    static void Main()
    {
        // Path of the destination PDF that will receive inserted pages
        const string destinationPath = "destination.pdf";

        // Ensure the destination PDF exists; create a minimal PDF with one blank page if it does not
        if (!File.Exists(destinationPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add();               // Add a single blank page
                doc.Save(destinationPath);     // Save the newly created PDF
            }
        }

        // ---------------------------------------------------------------------
        // Helper: create a simple source PDF if it does not already exist.
        // This makes the sample self‑contained and prevents FileNotFoundException.
        // In evaluation mode Aspose.PDF can only hold a maximum of 4 elements in any
        // collection (pages, annotations, etc.). Therefore we cap the number of
        // pages we create to 4.
        // ---------------------------------------------------------------------
        void EnsureSourcePdf(string path, int requestedPageCount)
        {
            if (File.Exists(path)) return;
            // Cap page count to 4 to stay within evaluation‑mode limits
            int pageCount = Math.Min(requestedPageCount, 4);
            using (Document srcDoc = new Document())
            {
                for (int i = 0; i < pageCount; i++)
                {
                    srcDoc.Pages.Add();
                }
                srcDoc.Save(path);
            }
        }

        // Define the source PDFs together with the page ranges to insert and the insertion position.
        // All page numbers are limited to the 4‑page maximum.
        var sources = new[]
        {
            new { File = "source1.pdf", StartPage = 2, EndPage = 4, InsertAt = 1, PageCount = 4 },
            new { File = "source2.pdf", StartPage = 1, EndPage = 3, InsertAt = 3, PageCount = 4 },
            // Add additional source definitions as required (keep page counts ≤ 4)
        };

        // Make sure every source PDF exists (create a dummy one if necessary)
        foreach (var src in sources)
        {
            EnsureSourcePdf(src.File, src.PageCount);
        }

        // Temporary file used to hold the intermediate result after each insertion
        string tempPath = Path.GetTempFileName();

        foreach (var src in sources)
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block is required
            PdfFileEditor editor = new PdfFileEditor();

            // Insert the specified page range from src.File into the current destination PDF
            // The result is written to tempPath
            bool inserted = editor.Insert(
                destinationPath,          // inputFile: current destination PDF
                src.InsertAt,            // insertLocation: position where pages will be inserted (1‑based)
                src.File,                // portFile: source PDF containing pages to insert
                src.StartPage,           // startPage: first page of the range in the source PDF
                src.EndPage,             // endPage: last page of the range in the source PDF
                tempPath);               // outputFile: temporary file for the new PDF

            if (!inserted)
            {
                Console.Error.WriteLine($"Failed to insert pages from '{src.File}'.");
                break;
            }

            // Replace the destination PDF with the newly created temporary PDF for the next iteration
            File.Copy(tempPath, destinationPath, overwrite: true);

            // Evaluation mode limit: stop processing if the destination PDF already has 4 pages
            using (Document checkDoc = new Document(destinationPath))
            {
                if (checkDoc.Pages.Count >= 4)
                {
                    Console.WriteLine("Reached evaluation‑mode page limit (4 pages). Stopping further insertions.");
                    break;
                }
            }
        }

        // Clean up the temporary file used during processing
        if (File.Exists(tempPath))
        {
            File.Delete(tempPath);
        }

        Console.WriteLine($"Batch insertion completed. Result saved to '{destinationPath}'.");
    }
}
