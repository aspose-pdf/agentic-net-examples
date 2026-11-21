using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // PDF to which the background will be added
        const string backgroundImgPath = "background.jpg"; // Image used as background
        const string outputPdfPath = "output.pdf";         // Resulting PDF

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(backgroundImgPath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImgPath}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Apply the background image to every page.
            // Aspose.Pdf uses 1‑based page indexing.
            for (int i = 1; i <= srcDoc.Pages.Count; i++)
            {
                // Create an ImageStamp for the current page size.
                ImageStamp bgStamp = new ImageStamp(backgroundImgPath)
                {
                    Width = srcDoc.Pages[i].PageInfo.Width,
                    Height = srcDoc.Pages[i].PageInfo.Height,
                    Background = true // Ensure the image is placed behind existing content
                };

                srcDoc.Pages[i].AddStamp(bgStamp);
            }

            // Save the modified PDF. No SaveOptions needed because the output format is PDF.
            srcDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background image added to all pages. Output saved to '{outputPdfPath}'.");
    }
}
