using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampImagePath = "stamp.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create an image stamp
            ImageStamp imgStamp = new ImageStamp(stampImagePath);

            // Set a precise zoom factor using double precision
            double preciseZoom = 1.23456789;
            imgStamp.Zoom = preciseZoom;

            // Position the stamp – use XIndent/YIndent for absolute coordinates
            imgStamp.XIndent = 100.0; // distance from the left edge
            imgStamp.YIndent = 100.0; // distance from the bottom edge

            // Control whether the stamp is placed in the background or foreground
            // In Aspose.Pdf.ImageStamp the property is named "Background"
            imgStamp.Background = false; // false = foreground (over content)

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with precise zoom to '{outputPath}'.");
    }
}
