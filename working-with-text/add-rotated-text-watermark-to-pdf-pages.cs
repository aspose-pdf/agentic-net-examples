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

        // Load the PDF document (using rule‑based load)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing per rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text fragment that will serve as the watermark
                TextFragment tf = new TextFragment("CONFIDENTIAL");
                tf.Position = new Position(200, 400);               // place near centre
                tf.TextState.Font = FontRepository.FindFont("Arial");
                tf.TextState.FontSize = 72;
                tf.TextState.ForegroundColor = Color.Red;           // use Aspose.Pdf.Color
                tf.TextState.Rotation = 45;                         // rotate 45 degrees

                // Append the fragment to the current page using TextBuilder
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(tf);
            }

            // Save the modified PDF (using rule‑based save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}