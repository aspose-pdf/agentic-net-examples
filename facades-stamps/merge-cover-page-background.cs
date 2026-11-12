using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string coverPath = "cover.pdf";               // PDF to be used as background
        const string mainPath = "main.pdf";                 // Main document
        const string outputPath = "merged_with_cover.pdf"; // Resulting PDF

        // Verify input files exist
        if (!File.Exists(coverPath))
        {
            Console.Error.WriteLine($"Cover PDF not found: {coverPath}");
            return;
        }
        if (!File.Exists(mainPath))
        {
            Console.Error.WriteLine($"Main PDF not found: {mainPath}");
            return;
        }

        try
        {
            // Bind the main document to the stamp processor
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                fileStamp.BindPdf(mainPath);

                // Prepare a stamp that uses the cover PDF (first page) as background
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                // Source PDF and the page of the source to use as the stamp
                stamp.BindPdf(coverPath, 1);
                // Apply the stamp to every page of the target document
                int pageCount = GetPageCount(mainPath);
                stamp.Pages = new int[pageCount];
                for (int i = 0; i < pageCount; i++)
                    stamp.Pages[i] = i + 1; // pages are 1‑based
                // Place the stamp behind existing page content
                stamp.IsBackground = true;

                // Add the configured stamp to the target PDF
                fileStamp.AddStamp(stamp);

                // Save the merged PDF
                fileStamp.Save(outputPath);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to obtain the number of pages in a PDF
    private static int GetPageCount(string pdfPath)
    {
        using (Document doc = new Document(pdfPath))
        {
            return doc.Pages.Count;
        }
    }
}