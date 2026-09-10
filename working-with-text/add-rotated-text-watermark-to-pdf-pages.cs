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
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextBuilder for the current page
                TextBuilder builder = new TextBuilder(page);

                // Create the watermark text fragment
                TextFragment watermark = new TextFragment("CONFIDENTIAL");

                // Position the text roughly at the center of the page
                // (you may adjust X/Y as needed)
                watermark.Position = new Position(page.PageInfo.Width / 2, page.PageInfo.Height / 2);

                // Set visual appearance
                watermark.TextState.Font = FontRepository.FindFont("Helvetica");
                watermark.TextState.FontSize = 72;
                watermark.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

                // Rotate the text (45 degrees)
                watermark.TextState.Rotation = 45;

                // Optionally send the watermark behind page content
                watermark.ZIndex = -1; // negative ZIndex places it behind other graphics

                // Append the fragment to the page
                builder.AppendText(watermark);
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}