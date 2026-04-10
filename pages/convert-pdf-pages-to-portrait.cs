using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "portrait_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;

                // If the page is landscape (width > height), swap dimensions to make it portrait
                if (mediaBox.Width > mediaBox.Height)
                {
                    // Set new page size: height becomes width, width becomes height
                    page.SetPageSize(mediaBox.Height, mediaBox.Width);
                }
                // If already portrait, no action needed
            }

            // Save the modified document (lifecycle rule: Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages converted to portrait orientation and saved to '{outputPath}'.");
    }
}