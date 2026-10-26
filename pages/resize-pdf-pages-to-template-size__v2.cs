using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string sourcePath   = "source.pdf";
        const string outputPath   = "resized.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source not found: {sourcePath}");
            return;
        }

        // Load the reference PDF and obtain its first page dimensions.
        double templateWidth, templateHeight;
        using (Document templateDoc = new Document(templatePath))
        {
            // Assuming all template pages share the same size.
            Page templatePage = templateDoc.Pages[1];
            templateWidth  = templatePage.PageInfo.Width;
            templateHeight = templatePage.PageInfo.Height;
        }

        // Load the PDF to be resized, adjust each page, and save the result.
        using (Document sourceDoc = new Document(sourcePath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            for (int i = 1; i <= sourceDoc.Pages.Count; i++)
            {
                Page page = sourceDoc.Pages[i];
                // Resize the page to match the template dimensions.
                page.SetPageSize(templateWidth, templateHeight);
            }

            sourceDoc.Save(outputPath);
        }

        Console.WriteLine($"All pages resized and saved to '{outputPath}'.");
    }
}