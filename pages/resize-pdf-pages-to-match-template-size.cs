using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string inputPath    = "input.pdf";
        const string outputPath   = "output.pdf";

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

        // Load the template and the document to be resized
        using (Document templateDoc = new Document(templatePath))
        using (Document targetDoc   = new Document(inputPath))
        {
            // Use the size of the first page of the template as the target size
            Page templatePage = templateDoc.Pages[1];
            double targetWidth  = templatePage.PageInfo.Width;
            double targetHeight = templatePage.PageInfo.Height;

            // Apply the target size to every page in the target document
            foreach (Page page in targetDoc.Pages)
            {
                page.SetPageSize(targetWidth, targetHeight);
            }

            // Save the resized document
            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"All pages resized to match the template and saved as '{outputPath}'.");
    }
}