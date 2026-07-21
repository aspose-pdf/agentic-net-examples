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
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text fragment for the watermark
                TextFragment watermark = new TextFragment("CONFIDENTIAL");
                // Position the text (center of the page)
                watermark.Position = new Position(page.PageInfo.Width / 2, page.PageInfo.Height / 2);
                // Set visual properties
                watermark.TextState.FontSize = 72;
                watermark.TextState.Font = FontRepository.FindFont("Helvetica");
                watermark.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
                // Rotate the text (e.g., 45 degrees)
                watermark.TextState.Rotation = 45;

                // Append the watermark to the current page using TextBuilder
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}