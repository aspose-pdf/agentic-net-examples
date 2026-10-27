using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string templatePath = "template.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Load the template PDF and obtain its first page dimensions
        using (Document templateDoc = new Document(templatePath))
        {
            if (templateDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Template PDF contains no pages.");
                return;
            }

            // Aspose.Pdf uses 1‑based page indexing
            Page tmplPage = templateDoc.Pages[1];
            double targetWidth = tmplPage.PageInfo.Width;
            double targetHeight = tmplPage.PageInfo.Height;

            // Load the source PDF and resize each page to match the template size
            using (Document sourceDoc = new Document(sourcePath))
            {
                for (int i = 1; i <= sourceDoc.Pages.Count; i++)
                {
                    Page srcPage = sourceDoc.Pages[i];
                    srcPage.SetPageSize(targetWidth, targetHeight);
                }

                // Save the modified document
                sourceDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"All pages resized and saved to '{outputPath}'.");
    }
}