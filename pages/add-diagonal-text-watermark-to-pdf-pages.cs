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
        const string watermark   = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextStamp with the desired watermark text
                TextStamp stamp = new TextStamp(watermark);

                // Configure stamp appearance
                stamp.Background   = true;               // place behind page content
                stamp.Opacity      = 0.2;                // semi‑transparent
                stamp.RotateAngle  = -45;                // diagonal orientation
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Center;

                // Set text style
                stamp.TextState.Font       = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize   = 72;
                stamp.TextState.ForegroundColor = Color.Red;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}