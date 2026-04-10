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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Build the watermark text with the current year
            string watermarkText = $"© {DateTime.Now.Year}";

            // Create a TextStamp with the watermark text
            TextStamp stamp = new TextStamp(watermarkText);

            // Configure visual appearance of the stamp
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 48;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
            stamp.Opacity = 0.5f;                     // semi‑transparent
            stamp.Background = true;                  // place behind page content
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to every page (Aspose.Pdf uses 1‑based page indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}