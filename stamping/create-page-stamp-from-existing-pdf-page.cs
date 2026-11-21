using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string sourcePdfPath = "template.pdf";   // PDF containing the page to use as a stamp
        const string targetPdfPath = "document.pdf";   // PDF to which the stamp will be applied
        const string outputPdfPath = "document_with_stamp.pdf";

        // Verify files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        // Load the source PDF and extract the page to be used as a stamp
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page stampPage = sourceDoc.Pages[1];

            // Create a PdfPageStamp from the extracted page (core API, no Facades)
            PdfPageStamp pageStamp = new PdfPageStamp(stampPage)
            {
                // Optional: configure appearance
                Background = false,   // false = foreground stamp
                Zoom = 1.0            // 100% size
            };

            // Load the target PDF where the stamp will be placed
            using (Document targetDoc = new Document(targetPdfPath))
            {
                // Apply the stamp to every page of the target document
                for (int i = 1; i <= targetDoc.Pages.Count; i++)
                {
                    targetDoc.Pages[i].AddStamp(pageStamp);
                }

                // Save the modified document
                targetDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Stamp applied and saved to '{outputPdfPath}'.");
    }
}
