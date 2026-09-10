using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired watermark text
            TextStamp stamp = new TextStamp("CONFIDENTIAL");

            // Rotate the stamp 30 degrees to achieve a slanted watermark
            stamp.RotateAngle = 30;

            // Optional visual settings for better appearance
            stamp.Opacity = 0.3; // semi‑transparent
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Center;
            stamp.TextState.FontSize = 72;
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

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