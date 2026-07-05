using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath  = "logo.png";

        // Unicode alternative text (e.g., Japanese, English, Arabic)
        const string altText = "ロゴ画像 – Logo image – صورة الشعار";

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

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(imagePath);

            // Set Unicode alternative text for accessibility
            imgStamp.AlternativeText = altText;

            // Optional visual settings
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imgStamp.VerticalAlignment   = VerticalAlignment.Top;
            imgStamp.Opacity = 0.8f; // semi‑transparent

            // Add the stamp to the first page (Page.AddStamp)
            Page page = doc.Pages[1];
            page.AddStamp(imgStamp);

            // Save the modified PDF (document-disposal-with-using rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp with alternative text saved to '{outputPath}'.");
    }
}