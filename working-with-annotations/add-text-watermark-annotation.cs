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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text stamp that will act as a watermark
                TextStamp stamp = new TextStamp("Confidential")
                {
                    // Center horizontally, align to the top of the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Top,
                    // Use XIndent/YIndent instead of the non‑existent Margin property
                    XIndent = 0,               // no horizontal offset (centered)
                    YIndent = 10               // small offset from the top edge
                };

                // Configure the appearance: bold font, suitable size
                stamp.TextState.Font      = FontRepository.FindFont("Arial");
                stamp.TextState.FontSize  = 24;
                stamp.TextState.FontStyle = FontStyles.Bold;
                stamp.TextState.ForegroundColor = Color.FromRgb(0.8, 0.0, 0.0); // optional red color

                // Apply the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
