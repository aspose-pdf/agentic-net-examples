using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string imagePath  = "logo.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdf = new Document(inputPath))
        {
            // Create an image stamp with 20% opacity
            ImageStamp stamp = new ImageStamp(imagePath)
            {
                Opacity = 0.2,                     // 20 percent opacity
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                Background = false                 // stamp appears on top of page content
            };

            // Apply the stamp to each page individually
            foreach (Page page in pdf.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            pdf.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}