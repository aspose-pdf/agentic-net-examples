using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextState if needed (optional)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "confidential_output.pdf";

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

                // Create a simple text stamp with the desired label
                TextStamp stamp = new TextStamp("CONFIDENTIAL")
                {
                    // Set the opacity to 0.6 for a subtle appearance
                    Opacity = 0.6,

                    // Optional: position the stamp in the center of the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,

                    // Ensure the stamp is drawn on top of the page content
                    Background = false
                };

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved stamped PDF to '{outputPath}'.");
    }
}