using System;
using System.IO;
using Aspose.Pdf; // Core API namespace

class Program
{
    static void Main()
    {
        const string targetPdfPath   = "target.pdf";   // PDF to which the background will be applied
        const string sourcePdfPath   = "background.pdf"; // PDF containing the page used as background
        const string outputPdfPath   = "result.pdf";

        // Verify input files exist
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Background source file not found: {sourcePdfPath}");
            return;
        }

        try
        {
            // Load the document that will receive the background stamp
            using (Document targetDoc = new Document(targetPdfPath))
            {
                // Create a PdfPageStamp from the first page of the source PDF.
                // Constructor (string fileName, int pageIndex) uses 1‑based page indexing.
                Aspose.Pdf.PdfPageStamp backgroundStamp = new Aspose.Pdf.PdfPageStamp(sourcePdfPath, 1);
                backgroundStamp.Background = true; // Place stamp behind existing content

                // Apply the stamp to every page of the target document
                foreach (Page page in targetDoc.Pages)
                {
                    page.AddStamp(backgroundStamp);
                }

                // Save the modified document
                targetDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Background stamp applied successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}