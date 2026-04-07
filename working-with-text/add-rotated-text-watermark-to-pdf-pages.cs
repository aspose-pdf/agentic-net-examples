using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text fragment that will serve as the watermark
                TextFragment watermark = new TextFragment("CONFIDENTIAL");

                // Position the fragment at the center of the page
                double centerX = page.PageInfo.Width / 2;
                double centerY = page.PageInfo.Height / 2;
                watermark.Position = new Position(centerX, centerY);

                // Set visual appearance
                watermark.TextState.Font = FontRepository.FindFont("Helvetica");
                watermark.TextState.FontSize = 72;
                watermark.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                watermark.TextState.Rotation = 45; // Rotate 45 degrees

                // Append the fragment to the current page using TextBuilder
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(watermark);
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}