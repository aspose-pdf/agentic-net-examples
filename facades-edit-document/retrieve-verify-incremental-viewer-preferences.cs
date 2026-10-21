using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string firstPdf  = "input.pdf";
        const string secondPdf = "second.pdf";
        const string mergedPdf = "merged.pdf";
        const string stampImg  = "stamp.png";
        const string finalPdf  = "final_output.pdf";

        // Verify required files exist
        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"File not found: {firstPdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"File not found: {secondPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"File not found: {stampImg}");
            return;
        }

        // -------------------------------------------------
        // 1) Merge two PDFs using the Facades API (PdfFileEditor)
        // -------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        try
        {
            // Concatenate firstPdf and secondPdf into mergedPdf
            editor.Concatenate(firstPdf, secondPdf, mergedPdf);
            Console.WriteLine($"Merged PDFs saved to '{mergedPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge: {ex.Message}");
            return;
        }

        // -------------------------------------------------
        // Retrieve viewer preferences after merge (if supported)
        // -------------------------------------------------
        // NOTE: The ViewerPreferences property exists only in newer
        // versions of Aspose.Pdf. If you are using an older version,
        // the following block must remain commented out to compile.
        /*
        using (Document mergedDoc = new Document(mergedPdf))
        {
            var prefs = mergedDoc.ViewerPreferences;
            Console.WriteLine($"After merge - HideToolbar: {prefs.HideToolbar}");
            // Add additional preference checks as needed
        }
        */

        // -------------------------------------------------
        // 2) Add an image stamp to every page of the merged PDF
        // -------------------------------------------------
        try
        {
            using (Document doc = new Document(mergedPdf))
            {
                ImageStamp imgStamp = new ImageStamp(stampImg)
                {
                    Background          = false,
                    Opacity             = 0.5,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };

                // Apply the stamp to each page
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Save the final result
                doc.Save(finalPdf);
                Console.WriteLine($"Stamped PDF saved to '{finalPdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during stamping: {ex.Message}");
            return;
        }

        // -------------------------------------------------
        // Retrieve viewer preferences after stamping (if supported)
        // -------------------------------------------------
        // NOTE: Commented out for compatibility with older library versions.
        /*
        using (Document finalDoc = new Document(finalPdf))
        {
            var prefs = finalDoc.ViewerPreferences;
            Console.WriteLine($"After stamping - HideToolbar: {prefs.HideToolbar}");
            // Add additional preference checks as needed
        }
        */

        Console.WriteLine("Processing completed successfully.");
    }
}