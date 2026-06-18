using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // List to hold IDs of images that lack alternative text
        var missingAltIds = new List<string>();

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all XImage objects on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Retrieve any alternative text associated with this image on this page
                    List<string> altTexts = img.GetAlternativeText(page);

                    // If no alternative text is present, record the image identifier
                    if (altTexts == null || altTexts.Count == 0)
                    {
                        // GetNameInCollection returns the resource name (ID) of the image
                        string imageId = img.GetNameInCollection();
                        missingAltIds.Add(imageId);
                    }
                }
            }
        }

        // Output the collected image IDs
        Console.WriteLine("Images missing alternative text:");
        foreach (string id in missingAltIds)
        {
            Console.WriteLine(id);
        }
    }
}