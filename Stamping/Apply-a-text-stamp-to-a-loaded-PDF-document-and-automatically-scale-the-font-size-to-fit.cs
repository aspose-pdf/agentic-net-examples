using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a new TextStamp for each page
                TextStamp stamp = new TextStamp("Confidential")
                {
                    // Enable automatic font size adjustment to fit the stamp rectangle
                    AutoAdjustFontSizeToFitStampRectangle = true,

                    // Optional: position the stamp (e.g., centered at the top)
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Top,

                    // Optional: set margins if needed
                    TopMargin    = 20,
                    BottomMargin = 20,
                    LeftMargin   = 20,
                    RightMargin  = 20
                };

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp applied and saved to '{outputPath}'.");
    }
}