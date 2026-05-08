using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePath = "template.pdf";   // PDF whose page size will be used
        const string sourcePath   = "input.pdf";      // PDF whose pages will be resized
        const string outputPath   = "output_resized.pdf";

        // Verify files exist
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Load the template PDF and obtain its first page dimensions
        double targetWidth, targetHeight;
        using (Document templateDoc = new Document(templatePath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page templatePage = templateDoc.Pages[1];
            targetWidth  = templatePage.PageInfo.Width;
            targetHeight = templatePage.PageInfo.Height;
        }

        // Load the source PDF, resize each page, and save the result
        using (Document sourceDoc = new Document(sourcePath))
        {
            for (int i = 1; i <= sourceDoc.Pages.Count; i++)
            {
                // Set each page size to match the template dimensions
                sourceDoc.Pages[i].SetPageSize(targetWidth, targetHeight);
            }

            // Save the modified document
            sourceDoc.Save(outputPath);
        }

        Console.WriteLine($"All pages resized to match template. Saved as '{outputPath}'.");
    }
}