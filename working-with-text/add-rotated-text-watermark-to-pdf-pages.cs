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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a text fragment that will serve as the watermark
                TextFragment watermark = new TextFragment("CONFIDENTIAL");
                // Position the text (coordinates are in points)
                watermark.Position = new Position(200, 400);
                // Set visual appearance
                watermark.TextState.Font = FontRepository.FindFont("Helvetica");
                watermark.TextState.FontSize = 72;
                watermark.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                // Rotate the text (e.g., 45 degrees)
                watermark.TextState.Rotation = 45;

                // Append the text fragment to the current page using TextBuilder
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(watermark);
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}