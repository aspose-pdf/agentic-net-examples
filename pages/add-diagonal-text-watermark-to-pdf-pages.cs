using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with the desired watermark text
            TextStamp stamp = new TextStamp(watermarkText);

            // Configure stamp appearance for a diagonal, semi‑transparent watermark
            stamp.Background   = true;               // place behind page content
            stamp.Opacity      = 0.2f;               // semi‑transparent
            stamp.RotateAngle  = -45;                // diagonal orientation
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Make the stamp cover the whole page (use size of the first page as reference)
            stamp.Width  = doc.Pages[1].PageInfo.Width;
            stamp.Height = doc.Pages[1].PageInfo.Height;

            // Set text styling
            stamp.TextState.Font       = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize   = 72;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the watermarked PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}