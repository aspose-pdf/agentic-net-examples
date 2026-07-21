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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a textual stamp that will serve as the watermark
            TextStamp watermark = new TextStamp("CONFIDENTIAL");

            // Configure the visual appearance of the stamp
            watermark.TextState.Font = FontRepository.FindFont("Helvetica");
            watermark.TextState.FontSize = 72;
            watermark.TextState.ForegroundColor = Aspose.Pdf.Color.Gray; // fill color

            // Semi‑transparent fill
            watermark.Opacity = 0.5f;

            // Outline (stroke) settings
            watermark.OutlineOpacity = 0.5f;   // semi‑transparent outline
            watermark.OutlineWidth   = 1.0f;   // outline thickness

            // Place the stamp behind the page content (typical for watermarks)
            watermark.Background = true;

            // Apply the watermark to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}