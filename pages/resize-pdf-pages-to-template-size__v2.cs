using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string referencePath = "template.pdf";
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(referencePath))
        {
            Console.Error.WriteLine($"Reference file not found: {referencePath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the reference PDF to obtain the desired page dimensions.
        using (Document referenceDoc = new Document(referencePath))
        {
            // Use the first page of the template as the size source.
            Page refPage = referenceDoc.Pages[1];
            Aspose.Pdf.Rectangle refRect = refPage.Rect;

            // Create a PageSize object from the reference rectangle.
            PageSize targetSize = new PageSize((float)refRect.Width, (float)refRect.Height);

            // Load the document whose pages need to be resized.
            using (Document targetDoc = new Document(inputPath))
            {
                // Pages collection is 1‑based; iterate through all pages.
                for (int i = 1; i <= targetDoc.Pages.Count; i++)
                {
                    Page page = targetDoc.Pages[i];
                    // Resize each page to match the template size.
                    page.Resize(targetSize);
                }

                // Save the resized PDF.
                targetDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}