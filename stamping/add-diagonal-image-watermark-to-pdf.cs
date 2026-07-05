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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Apply the diagonal image stamp to every page
            foreach (Page page in doc.Pages)
            {
                // Create an ImageStamp from the image file
                ImageStamp imgStamp = new ImageStamp(imagePath);

                // Size the stamp to cover the whole page (optional)
                imgStamp.Width  = page.PageInfo.Width;
                imgStamp.Height = page.PageInfo.Height;

                // Position the stamp at the page origin
                imgStamp.XIndent = 0;
                imgStamp.YIndent = 0;

                // Rotate 90 degrees to achieve a diagonal watermark
                imgStamp.RotateAngle = 90; // arbitrary angle in degrees

                // Make the watermark semi‑transparent and place it behind page content
                imgStamp.Opacity   = 0.3;
                imgStamp.Background = true;

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}