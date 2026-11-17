using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logoPath = "logo.png";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the watermark will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Create an ImageStamp from the PNG logo
            ImageStamp logoStamp = new ImageStamp(logoPath)
            {
                // Set the desired size of the logo (optional – adjust as needed)
                Width = 150,
                Height = 150,

                // Position the stamp – here we centre it on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,

                // Make the logo semi‑transparent
                Opacity = 0.5,

                // Rotate 30 degrees clockwise
                RotateAngle = 30
            };

            // Add the stamp to the page
            page.AddStamp(logoStamp);

            // Save the result
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
