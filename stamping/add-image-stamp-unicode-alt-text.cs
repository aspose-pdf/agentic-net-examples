using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPath = "output.pdf";

        // Verify that the source PDF and stamp image exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document (using ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath);

            // Set Unicode alternative text for multilingual accessibility
            imgStamp.AlternativeText = "示例图像 - Example Image - مثال صورة";

            // Optional positioning and appearance settings
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imgStamp.VerticalAlignment   = VerticalAlignment.Top;
            imgStamp.Opacity = 0.8; // semi‑transparent

            // Add the stamp to the first page (pages are 1‑based)
            Page firstPage = doc.Pages[1];
            firstPage.AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with image stamp: {outputPath}");
    }
}