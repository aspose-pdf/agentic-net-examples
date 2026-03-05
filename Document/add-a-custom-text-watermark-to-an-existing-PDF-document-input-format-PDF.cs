using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextStamp, TextState, FontRepository, Color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextStamp with the custom watermark text
                TextStamp stamp = new TextStamp(watermarkText);

                // Configure appearance via TextState
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize = 72;
                // Use Aspose.Pdf.Color to stay cross‑platform
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                stamp.Opacity = 0.3; // semi‑transparent watermark

                // Center the watermark on the page
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment = VerticalAlignment.Center;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}