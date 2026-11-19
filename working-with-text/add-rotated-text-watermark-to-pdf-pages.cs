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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextFragment that will serve as the watermark
                TextFragment watermark = new TextFragment("CONFIDENTIAL");
                watermark.TextState.Font      = FontRepository.FindFont("Arial");
                watermark.TextState.FontSize  = 72;
                watermark.TextState.FontStyle = FontStyles.Bold;
                watermark.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                // Rotate the text 45 degrees
                watermark.TextState.Rotation = 45;

                // Position the watermark near the centre of the page
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;
                watermark.Position = new Position(pageWidth / 2, pageHeight / 2);

                // Add the fragment to the current page using TextBuilder
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(watermark);
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}