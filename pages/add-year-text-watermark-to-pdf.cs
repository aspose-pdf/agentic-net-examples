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
            // Build watermark text that includes the current year
            string watermarkText = $"© {DateTime.Now.Year} Confidential";

            // Create a TextStamp with the watermark text
            TextStamp stamp = new TextStamp(watermarkText);
            // Configure text appearance
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 36;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
            // Make the stamp semi‑transparent and place it behind page content
            stamp.Opacity = 0.5f;
            // Use the correct property name for background placement
            stamp.Background = true; // 'IsBackground' does not exist in recent versions
            // Center the stamp on each page
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Center;

            // Apply the stamp to every page (pages are 1‑based)
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
