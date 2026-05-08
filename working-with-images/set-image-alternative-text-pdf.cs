using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged; // for ITaggedContent if needed (not required here)

class SetImageAltText
{
    static void Main()
    {
        // Input PDF containing images
        const string inputPath = "input.pdf";
        // Output PDF with alternative text set
        const string outputPath = "output_with_alt.pdf";
        // Desired alternative text for all images
        const string altText = "Descriptive text for the image";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over each XImage resource on the page
                foreach (XImage img in page.Resources.Images)
                {
                    // Try to set the alternative text; returns true if successful
                    bool success = img.TrySetAlternativeText(altText, page);
                    if (!success)
                    {
                        Console.WriteLine($"Could not set alt text for an image on page {i}.");
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with alternative text: {outputPath}");
    }
}