using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Chapters";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // -----------------------------------------------------------------
        // Step 1: Load the source PDF and resize every page to a uniform size
        // (A4 = 595 x 842 points). Use the PageInfo object – PdfPageEditor no longer
        // provides a SetPageSize method in recent Aspose.Pdf versions.
        // -----------------------------------------------------------------
        string resizedPdfPath = Path.Combine(outputFolder, "resized.pdf");

        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Resize each page to A4 dimensions (points)
            foreach (Page page in srcDoc.Pages)
            {
                page.PageInfo.Width = 595;   // A4 width in points
                page.PageInfo.Height = 842;  // A4 height in points
                page.PageInfo.IsLandscape = false; // ensure portrait orientation
            }

            // Save the uniformly formatted PDF to a temporary file
            srcDoc.Save(resizedPdfPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Split the resized PDF into separate chapter files.
        // For demonstration, each chapter consists of a fixed number of pages.
        // PdfFileEditor.Extract(source, startPage, endPage, destination) is used.
        // -----------------------------------------------------------------
        const int pagesPerChapter = 10; // adjust as needed for real chapter boundaries
        PdfFileEditor fileEditor = new PdfFileEditor();

        using (Document resizedDoc = new Document(resizedPdfPath))
        {
            int totalPages = resizedDoc.Pages.Count;
            int chapterIndex = 1;

            for (int startPage = 1; startPage <= totalPages; startPage += pagesPerChapter)
            {
                int endPage = Math.Min(startPage + pagesPerChapter - 1, totalPages);
                string chapterPath = Path.Combine(outputFolder, $"Chapter_{chapterIndex}.pdf");

                // Correct overload: source, startPage, endPage, destination
                fileEditor.Extract(resizedPdfPath, startPage, endPage, chapterPath);

                Console.WriteLine($"Created {chapterPath} (pages {startPage}-{endPage})");
                chapterIndex++;
            }
        }

        // Optional: clean up the intermediate resized PDF
        try { File.Delete(resizedPdfPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine("Splitting completed.");
    }
}
