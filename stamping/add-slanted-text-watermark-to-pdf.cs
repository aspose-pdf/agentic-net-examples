using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the watermark text
            TextStamp stamp = new TextStamp("CONFIDENTIAL");

            // Rotate the stamp 30 degrees for a slanted effect
            stamp.RotateAngle = 30;

            // Optional visual settings
            stamp.Opacity   = 0.3;                     // semi‑transparent
            stamp.Background = true;                    // draw behind page content
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}