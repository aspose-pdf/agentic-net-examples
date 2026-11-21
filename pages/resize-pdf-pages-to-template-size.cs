using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string inputPath    = "input.pdf";
        const string outputPath   = "output_resized.pdf";

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

        // Load the template PDF and obtain its first page dimensions
        using (Document templateDoc = new Document(templatePath))
        {
            if (templateDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Template PDF contains no pages.");
                return;
            }

            // Page indexing in Aspose.Pdf is 1‑based
            Page templatePage = templateDoc.Pages[1];
            double targetWidth  = templatePage.PageInfo.Width;
            double targetHeight = templatePage.PageInfo.Height;

            // Load the document whose pages will be resized
            using (Document sourceDoc = new Document(inputPath))
            {
                // Apply the template size to every page
                for (int i = 1; i <= sourceDoc.Pages.Count; i++)
                {
                    Page page = sourceDoc.Pages[i];
                    page.PageInfo.Width  = targetWidth;
                    page.PageInfo.Height = targetHeight;
                }

                // Save the resized PDF
                sourceDoc.Save(outputPath);
                Console.WriteLine($"All pages resized and saved to '{outputPath}'.");
            }
        }
    }
}