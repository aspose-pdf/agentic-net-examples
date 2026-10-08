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
        const string watermark  = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Create a text fragment for the watermark
                    TextFragment tf = new TextFragment(watermark);

                    // Center the watermark on the page
                    tf.Position = new Position(page.PageInfo.Width / 2, page.PageInfo.Height / 2);
                    tf.HorizontalAlignment = HorizontalAlignment.Center;
                    tf.VerticalAlignment   = VerticalAlignment.Center;

                    // Configure appearance: semi‑transparent fill and simulated outline
                    tf.TextState.FontSize = 72;
                    // Use a bold font to give the impression of an outline
                    tf.TextState.Font = FontRepository.FindFont("Arial-BoldMT");
                    // Semi‑transparent fill (alpha 80 out of 255 ≈ 0.31 opacity)
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.FromArgb(80, 204, 204, 204);

                    // Add the watermark to the current page
                    page.Paragraphs.Add(tf);
                }

                // Save the modified document as PDF (default Save writes PDF)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
