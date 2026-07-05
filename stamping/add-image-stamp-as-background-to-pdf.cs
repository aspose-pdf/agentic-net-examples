using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and image to be used as a stamp
        const string inputPdfPath  = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPdfPath = "output.pdf";

        // Verify that required files exist
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

        // Load the PDF document (using block ensures proper disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp – this stamp will render the image on the page
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Set the stamp to appear behind the page content
                Background = true,

                // Optional visual settings
                Opacity = 0.5,                                 // 50% transparent
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the stamp to every page in the document
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamp applied and saved to '{outputPdfPath}'.");
    }
}