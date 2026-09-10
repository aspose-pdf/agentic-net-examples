using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for TextState if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_page_numbers.pdf";

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

                // Create a PageNumberStamp – default format is "#"
                PageNumberStamp pageNumberStamp = new PageNumberStamp();

                // Position the stamp in the footer (centered, bottom of the page)
                pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
                pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
                pageNumberStamp.BottomMargin        = 20;   // distance from the bottom edge

                // Optional: adjust appearance
                pageNumberStamp.TextState.FontSize = 12;
                pageNumberStamp.TextState.Font      = FontRepository.FindFont("Helvetica");
                pageNumberStamp.TextState.ForegroundColor = Color.Black;

                // Add the stamp to the current page
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}