using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextStamp and related types

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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a textual stamp with the desired watermark text
            TextStamp stamp = new TextStamp("CONFIDENTIAL");

            // Rotate the stamp by 30 degrees to achieve a slanted effect
            stamp.RotateAngle = 30;

            // Optional visual settings
            stamp.Opacity = 0.3;                     // semi‑transparent
            stamp.Background = false;                // draw on top of page content
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to every page in the document
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