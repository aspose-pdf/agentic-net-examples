using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF file to be inspected
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // List that will hold the IDs (names) of images missing alternative text
        List<string> imagesMissingAlt = new List<string>();

        // Load the PDF document (no special load options required for a plain PDF)
        using (Document doc = new Document(pdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over each XImage resource on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Retrieve any alternative text associated with this image on the page
                    List<string> altTexts = img.GetAlternativeText(page);

                    // If no alternative text is present, record the image identifier
                    if (altTexts == null || altTexts.Count == 0)
                    {
                        // Use the image name within the PDF as its identifier
                        // GetNameInCollection returns the key used in the image collection
                        string imageId = img.GetNameInCollection();
                        imagesMissingAlt.Add(imageId);
                    }
                }
            }
        }

        // Output the result
        Console.WriteLine("Images missing alternative text:");
        foreach (string id in imagesMissingAlt)
        {
            Console.WriteLine($"- {id}");
        }
    }
}