using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePath = "template.pdf";   // reference PDF with desired page size
        const string inputPath    = "input.pdf";      // PDF to be resized
        const string outputPath   = "resized_output.pdf";

        // Verify files exist
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        // Load the template PDF and obtain the size of its first page
        PageSize targetSize;
        using (Document templateDoc = new Document(templatePath))
        {
            // Pages are 1‑based; get the first page
            Page templatePage = templateDoc.Pages[1];

            // The page rectangle defines the media box (LLX, LLY, URX, URY)
            Aspose.Pdf.Rectangle rect = templatePage.Rect;
            // Aspose.Pdf.Rectangle properties are double – cast to float for PageSize
            float width  = (float)(rect.URX - rect.LLX);
            float height = (float)(rect.URY - rect.LLY);

            // Create a PageSize instance with the extracted dimensions
            targetSize = new PageSize(width, height);
        }

        // Load the PDF that needs to be resized
        using (Document srcDoc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based collection)
            foreach (Page page in srcDoc.Pages)
            {
                // Resize each page to match the template size
                page.Resize(targetSize);
            }

            // Save the resized document
            srcDoc.Save(outputPath);
        }

        Console.WriteLine($"All pages resized to template size and saved to '{outputPath}'.");
    }
}
