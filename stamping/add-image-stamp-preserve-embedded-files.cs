using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPdfPath = "output_stamped.pdf";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the original PDF. Embedded files are kept in the Document.EmbeddedFiles collection
        // and will be preserved when we save after stamping.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create an ImageStamp instance and configure its appearance.
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Place the stamp on top of page content (Background = false)
                Background = false,
                // Semi‑transparent stamp (optional)
                Opacity = 0.7,
                // Center the stamp horizontally and vertically on each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the stamp to every page individually.
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF. All original embedded files remain intact.
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdfPath}'. Embedded files preserved.");
    }
}