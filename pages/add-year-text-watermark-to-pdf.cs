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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp that includes the current year
            string watermarkText = $"Confidential © {DateTime.Now.Year}";
            TextStamp stamp = new TextStamp(watermarkText)
            {
                // Position the stamp in the center of the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Make the stamp semi‑transparent and place it behind page content
                Background          = true,
                Opacity             = 0.3f
            };

            // Configure text appearance via the existing TextState instance
            stamp.TextState.FontSize = 48;
            stamp.TextState.FontStyle = FontStyles.Bold;
            stamp.TextState.ForegroundColor = Color.Gray;

            // Apply the stamp to every page (Page.AddStamp, not PageCollection)
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                doc.Pages[i].AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
