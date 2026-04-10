using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF
        const string pdfPath = "input.pdf";
        // Path where the list of images missing Alt text will be saved
        const string outputPath = "missing_alt_images.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Collect identifiers of images that have no alternative text
        List<string> imagesMissingAlt = new List<string>();

        // Open the PDF document (Document implements IDisposable)
        using (Document doc = new Document(pdfPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over all XImage objects referenced on the page
                foreach (XImage img in page.Resources.Images)
                {
                    // Retrieve any alternative text associated with this image on the current page
                    List<string> altTexts = img.GetAlternativeText(page);

                    // If the list is null or empty, the image lacks Alt text
                    if (altTexts == null || altTexts.Count == 0)
                    {
                        // Use the image's name in the collection as its identifier
                        string imageId = img.Name ?? img.GetNameInCollection();
                        imagesMissingAlt.Add($"Page {pageIndex}: {imageId}");
                    }
                }
            }
        }

        // Persist the result – one line per image without Alt text
        File.WriteAllLines(outputPath, imagesMissingAlt);
        Console.WriteLine($"Found {imagesMissingAlt.Count} images without Alt text. Details saved to '{outputPath}'.");
    }
}