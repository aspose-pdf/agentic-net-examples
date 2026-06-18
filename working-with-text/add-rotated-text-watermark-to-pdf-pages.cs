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

                // Create a TextFragment that will serve as the watermark
                TextFragment tf = new TextFragment(watermarkText);

                // Position the fragment at the centre of the page
                tf.Position = new Position(page.PageInfo.Width / 2, page.PageInfo.Height / 2);

                // Configure visual appearance
                tf.TextState.FontSize = 72;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                // Use a semi‑transparent gray color (alpha 80 out of 255)
                tf.TextState.ForegroundColor = Color.FromArgb(80, 128, 128, 128);
                tf.TextState.Rotation = 45; // rotate 45 degrees

                // Append the fragment to the current page using TextBuilder
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(tf);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
