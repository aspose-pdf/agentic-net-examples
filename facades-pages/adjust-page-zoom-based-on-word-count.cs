using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "zoom_adjusted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (Document implements IDisposable)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Extract plain text from the current page
                TextAbsorber absorber = new TextAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                doc.Pages[pageNum].Accept(absorber);
                string pageText = absorber.Text ?? string.Empty;

                // Count words on the page
                int wordCount = pageText.Split(new char[] { ' ', '\r', '\n', '\t' },
                                             StringSplitOptions.RemoveEmptyEntries).Length;

                // Determine zoom factor: fewer words => higher zoom
                double zoomFactor;
                if (wordCount < 100)
                    zoomFactor = 1.5;   // high zoom for sparse pages
                else if (wordCount < 300)
                    zoomFactor = 1.2;   // medium zoom
                else
                    zoomFactor = 1.0;   // default zoom

                // Resize the page by scaling its width and height
                Page page = doc.Pages[pageNum];
                page.PageInfo.Width  = (float)(page.PageInfo.Width  * zoomFactor);
                page.PageInfo.Height = (float)(page.PageInfo.Height * zoomFactor);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Zoom‑adjusted PDF saved to '{outputPath}'.");
    }
}
