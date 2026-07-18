using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string referencePath = "template.pdf";   // PDF that defines the desired page size
        const string inputPath     = "input.pdf";      // PDF whose pages will be resized
        const string outputPath    = "output_resized.pdf";

        // Verify that both files exist before proceeding
        if (!File.Exists(referencePath))
        {
            Console.Error.WriteLine($"Reference PDF not found: {referencePath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        // Load the reference PDF to obtain the target page dimensions
        using (Document referenceDoc = new Document(referencePath))
        {
            // Use the first page of the reference as the size template
            Page referencePage = referenceDoc.Pages[1];
            double targetWidth  = referencePage.Rect.URX - referencePage.Rect.LLX;
            double targetHeight = referencePage.Rect.URY - referencePage.Rect.LLY;

            // Create a PageSize instance required by Page.Resize
            PageSize targetSize = new PageSize((float)targetWidth, (float)targetHeight);

            // Load the PDF that needs to be resized
            using (Document sourceDoc = new Document(inputPath))
            {
                // Resize each page to match the reference dimensions
                for (int i = 1; i <= sourceDoc.Pages.Count; i++)
                {
                    Page page = sourceDoc.Pages[i];
                    page.Resize(targetSize);
                }

                // Save the resized document
                sourceDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}