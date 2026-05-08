using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "watermarked.pdf";
        const string imagePath = "watermark.png"; // path to the image to use as watermark

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(imagePath);

            // Rotate the stamp 90 degrees (diagonal effect)
            imgStamp.RotateAngle = 90; // arbitrary angle in degrees

            // Optional visual settings
            imgStamp.Background = true;   // place the stamp behind page content
            imgStamp.Opacity   = 0.5;     // semi‑transparent

            // Scale the stamp to cover the whole page (optional)
            imgStamp.Width  = doc.Pages[1].PageInfo.Width;
            imgStamp.Height = doc.Pages[1].PageInfo.Height;
            imgStamp.XIndent = 0;
            imgStamp.YIndent = 0;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}