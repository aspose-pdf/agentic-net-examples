using System;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate over all images on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Retrieve alternative text for the image on this page
                    List<string> altTexts = img.GetAlternativeText(page);

                    if (altTexts != null && altTexts.Count > 0)
                    {
                        foreach (string alt in altTexts)
                        {
                            Console.WriteLine($"Page {page.Number}, Image \"{img.Name}\": Alt Text = \"{alt}\"");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Page {page.Number}, Image \"{img.Name}\": No alternative text.");
                    }
                }
            }
        }
    }
}