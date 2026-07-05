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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Build the watermark text with the current year
            string watermarkText = $"Confidential © {DateTime.Now.Year}";

            // Apply the watermark to every page using core Aspose.Pdf APIs (no Facades)
            foreach (Page page in doc.Pages)
            {
                // Create a TextFragment that will act as the watermark
                TextFragment fragment = new TextFragment(watermarkText)
                {
                    // Center the fragment on the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    // Optional: you can also set VerticalAlignment if needed (Aspose.Pdf does not expose it directly)
                };

                // Configure appearance
                fragment.TextState.Font = FontRepository.FindFont("Helvetica");
                fragment.TextState.FontSize = 48;
                // Semi‑transparent gray (alpha 77 ≈ 30% opacity)
                fragment.TextState.ForegroundColor = Color.FromArgb(77, 128, 128, 128);

                // Position the fragment roughly in the middle of the page
                fragment.Position = new Position(page.PageInfo.Width / 2, page.PageInfo.Height / 2);

                // Add the fragment to the page's paragraphs collection (background effect)
                page.Paragraphs.Add(fragment);
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
