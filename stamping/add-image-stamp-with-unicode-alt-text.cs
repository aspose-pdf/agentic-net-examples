using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath = "logo.png";
        const string altText = "示例图像 – Example Image – مثال صورة";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create an image stamp from the file
            ImageStamp imgStamp = new ImageStamp(imagePath);
            // Set Unicode alternative text for accessibility
            imgStamp.AlternativeText = altText;
            // Optional positioning and appearance settings
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imgStamp.VerticalAlignment = VerticalAlignment.Top;
            imgStamp.Opacity = 0.5f;

            // Add the stamp to the first page (pages are 1‑based)
            doc.Pages[1].AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp with alternative text saved to '{outputPath}'.");
    }
}