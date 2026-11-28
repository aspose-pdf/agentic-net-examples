using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string logoPath = "logo.png";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the target PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Create an image stamp from the logo using a FileStream
        using (FileStream imgStream = File.OpenRead(logoPath))
        {
            ImageStamp imgStamp = new ImageStamp(imgStream)
            {
                Background = false,
                // Align the image to the top centre of the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                // Top margin of 25 points (distance from the top edge)
                YIndent = 25
            };

            // Add the image stamp to every page in the document
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }
        }

        // Save the stamped PDF
        pdfDocument.Save(outputPdfPath);

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}
