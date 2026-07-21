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
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a textual stamp that will serve as the diagonal watermark
                TextStamp stamp = new TextStamp("CONFIDENTIAL")
                {
                    // Center the stamp on the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,

                    // Rotate the stamp by an arbitrary angle (45 degrees)
                    RotateAngle = 45,

                    // Make the stamp semi‑transparent
                    Opacity = 0.3,

                    // Ensure the stamp is drawn on top of page content
                    Background = false
                };

                // Configure visual appearance of the text
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize = 72;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}