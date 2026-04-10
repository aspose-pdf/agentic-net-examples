using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePath   = "source.pdf";   // PDF whose pages will be resized
        const string templatePath = "template.pdf"; // PDF providing the target page size
        const string outputPath   = "resized.pdf";

        if (!File.Exists(sourcePath) || !File.Exists(templatePath))
        {
            Console.Error.WriteLine("Source or template file not found.");
            return;
        }

        // Load the template PDF and obtain the size of its first page
        double targetWidth;
        double targetHeight;
        using (Document templateDoc = new Document(templatePath))
        {
            // PageInfo contains the dimensions of the page
            Page templatePage = templateDoc.Pages[1];
            targetWidth  = templatePage.PageInfo.Width;
            targetHeight = templatePage.PageInfo.Height;
        }

        // Load the source PDF, resize each page, and save the result
        using (Document sourceDoc = new Document(sourcePath))
        {
            for (int i = 1; i <= sourceDoc.Pages.Count; i++)
            {
                Page page = sourceDoc.Pages[i];
                // Set the page size to match the template dimensions
                page.SetPageSize(targetWidth, targetHeight);
            }

            sourceDoc.Save(outputPath);
        }

        Console.WriteLine($"All pages resized to match template. Saved as '{outputPath}'.");
    }
}