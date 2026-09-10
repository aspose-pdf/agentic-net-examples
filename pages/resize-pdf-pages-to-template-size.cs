using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePath = "template.pdf";   // PDF whose page size will be used
        const string inputPath    = "input.pdf";      // PDF whose pages will be resized
        const string outputPath   = "output_resized.pdf";

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

        // Load the template PDF and read the size of its first page
        double targetWidth, targetHeight;
        using (Document templateDoc = new Document(templatePath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page templatePage = templateDoc.Pages[1];
            // PageInfo provides the page dimensions
            targetWidth  = templatePage.PageInfo.Width;
            targetHeight = templatePage.PageInfo.Height;
        }

        // Load the PDF whose pages need to be resized
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Set each page size to match the template dimensions
                page.SetPageSize(targetWidth, targetHeight);
            }

            // Save the modified document – saving without explicit SaveOptions writes PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages resized to {targetWidth}×{targetHeight} and saved to '{outputPath}'.");
    }
}