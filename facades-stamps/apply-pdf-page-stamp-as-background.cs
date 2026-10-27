using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // ImageStamp lives here in recent versions

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // source PDF
        const string stampImagePath = "stamp.png"; // image to use as background stamp
        const string outputPdfPath = "stamped_output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the source PDF
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a single ImageStamp instance – it can be reused for all pages
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Set the stamp to be drawn behind the page content
                Background = true,
                // Optional positioning – here we centre the stamp on each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Apply the background stamp to every page
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the stamped PDF
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Background stamp applied to all pages. Output saved to '{outputPdfPath}'.");
    }
}
