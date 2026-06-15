using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";   // reference PDF with desired page size
        const string inputPath    = "input.pdf";      // PDF to be resized
        const string outputPath   = "resized_output.pdf";

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

        // Load the template to obtain target page dimensions
        using (Document templateDoc = new Document(templatePath))
        {
            // Assume the first page defines the desired size
            Page templatePage = templateDoc.Pages[1];
            double targetWidth  = templatePage.Rect.URX - templatePage.Rect.LLX;
            double targetHeight = templatePage.Rect.URY - templatePage.Rect.LLY;
            PageSize targetSize = new PageSize((float)targetWidth, (float)targetHeight);

            // Load the document to be resized
            using (Document sourceDoc = new Document(inputPath))
            {
                // Resize each page to match the template size
                for (int i = 1; i <= sourceDoc.Pages.Count; i++)
                {
                    Page page = sourceDoc.Pages[i];
                    page.Resize(targetSize); // Resizes the page to the target dimensions
                }

                // Save the resized document
                sourceDoc.Save(outputPath);
                Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
            }
        }
    }
}