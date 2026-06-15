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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a textual stamp with the desired watermark text
                TextStamp stamp = new TextStamp(watermarkText);

                // Configure visual appearance of the stamp
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");   // font
                stamp.TextState.FontSize = 72;                                 // large size
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;      // text color
                stamp.Opacity = 0.3;                                           // semi‑transparent
                stamp.Background = true;                                       // place behind page content
                stamp.RotateAngle = -45;                                       // diagonal orientation
                stamp.HorizontalAlignment = HorizontalAlignment.Center;       // center horizontally
                stamp.VerticalAlignment   = VerticalAlignment.Center;         // center vertically

                // Add the stamp to the current page (Page.AddStamp is the correct method)
                page.AddStamp(stamp);
            }

            // Save the modified document (lifecycle rule: use Save without extra options for PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}