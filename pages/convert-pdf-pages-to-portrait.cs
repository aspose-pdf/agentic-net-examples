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
            // Iterate over all pages (pages are 1‑based, but foreach abstracts that)
            foreach (Page page in doc.Pages)
            {
                // Calculate current width and height from the MediaBox
                double width  = page.MediaBox.URX - page.MediaBox.LLX;
                double height = page.MediaBox.URY - page.MediaBox.LLY;

                // If the page is landscape (width > height), swap dimensions to make it portrait
                if (width > height)
                {
                    // Set new page size with height as width and width as height
                    page.SetPageSize(height, width);
                }
            }

            // Save the modified document (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages converted to portrait orientation and saved to '{outputPath}'.");
    }
}