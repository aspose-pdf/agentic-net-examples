using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string coverPath   = "cover.pdf";   // PDF containing the cover page (used as background)
        const string mainPath    = "main.pdf";    // Main document to which the cover will be applied
        const string outputPath  = "merged.pdf"; // Resulting PDF

        // Verify input files exist
        if (!File.Exists(coverPath))
        {
            Console.Error.WriteLine($"Cover file not found: {coverPath}");
            return;
        }
        if (!File.Exists(mainPath))
        {
            Console.Error.WriteLine($"Main document not found: {mainPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Create a Stamp that represents the cover PDF page.
            //    The Stamp class (Aspose.Pdf.Facades) is the object that
            //    PdfFileStamp.AddStamp expects.
            // ------------------------------------------------------------
            var coverStamp = new Aspose.Pdf.Facades.Stamp();
            // Bind the first page of the cover PDF as the stamp content.
            coverStamp.BindPdf(coverPath, 1);
            // Place the stamp behind existing page content.
            coverStamp.IsBackground = true;
            // By default the stamp is applied to all pages of the bound PDF.
            // If you need a specific subset, assign an int[] to coverStamp.Pages.

            // ------------------------------------------------------------
            // 2. Apply the stamp to the main document.
            // ------------------------------------------------------------
            using (PdfFileStamp pdfStamp = new PdfFileStamp())
            {
                // Bind the main PDF that will receive the stamp.
                pdfStamp.BindPdf(mainPath);

                // Add the prepared stamp. The method accepts a single Stamp argument.
                pdfStamp.AddStamp(coverStamp);

                // Save the merged document.
                pdfStamp.Save(outputPath);
            }

            Console.WriteLine($"Cover merged successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during stamping: {ex.Message}");
        }
    }
}
