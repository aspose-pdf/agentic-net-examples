using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the template PDF, and the output PDF
        const string sourcePath   = "source.pdf";
        const string templatePath = "template.pdf";
        const string outputPath   = "output.pdf";

        // Verify that input files exist
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

        // Load both documents using the recommended using pattern (document-disposal-with-using rule)
        using (Document sourceDoc   = new Document(sourcePath))
        using (Document templateDoc = new Document(templatePath))
        {
            // Obtain the page size from the first page of the template PDF
            Page templatePage = templateDoc.Pages[1];
            double targetWidth  = templatePage.PageInfo.Width;
            double targetHeight = templatePage.PageInfo.Height;

            // Apply the template size to every page in the source PDF
            foreach (Page page in sourceDoc.Pages)
            {
                // SetPageSize directly (Page.SetPageSize method)
                page.SetPageSize(targetWidth, targetHeight);
                // Alternatively, you could use page.Resize(new PageSize((float)targetWidth, (float)targetHeight));
            }

            // Save the modified document (document-disposal-with-using rule)
            sourceDoc.Save(outputPath);
        }

        Console.WriteLine($"All pages resized to match template. Output saved to '{outputPath}'.");
    }
}