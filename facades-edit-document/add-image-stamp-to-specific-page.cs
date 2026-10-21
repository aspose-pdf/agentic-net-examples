using System;
using System.IO;
using Aspose.Pdf;                 // Core API for PDF manipulation
using Aspose.Pdf.Facades;        // Included as per task requirement (not used directly)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string stampImgPath  = "stamp.png";
        const string outputPdfPath = "output.pdf";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(stampImgPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImgPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdfPath))
            {
                // Ensure the document has at least two pages
                if (doc.Pages.Count < 2)
                {
                    Console.Error.WriteLine("The PDF does not contain a second page.");
                    return;
                }

                // Create an ImageStamp instance (core API) and configure its appearance
                Aspose.Pdf.ImageStamp imgStamp = new Aspose.Pdf.ImageStamp(stampImgPath)
                {
                    Background          = false,                                   // Draw over page content
                    HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,   // Center horizontally
                    VerticalAlignment   = Aspose.Pdf.VerticalAlignment.Center      // Center vertically
                };

                // Add the stamp to the second page (pages are 1‑based)
                doc.Pages[2].AddStamp(imgStamp);

                // Save the modified PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Stamp added to page 2 and saved as '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}