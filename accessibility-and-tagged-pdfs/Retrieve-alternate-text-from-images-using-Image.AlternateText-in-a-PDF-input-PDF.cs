using System;
using System.IO;
using System.Collections.Generic;
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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page (Aspose.Pdf uses 1‑based indexing internally)
            foreach (Page page in doc.Pages)
            {
                // Iterate over all XImage resources on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Retrieve the list of alternative texts for this image on this page
                    List<string> altTexts = img.GetAlternativeText(page);

                    if (altTexts != null && altTexts.Count > 0)
                    {
                        // Output each alternative text found
                        foreach (string alt in altTexts)
                        {
                            Console.WriteLine($"Page {page.Number}, Image \"{img.Name}\": Alt Text = \"{alt}\"");
                        }
                    }
                    else
                    {
                        // No alternative text associated with this image
                        Console.WriteLine($"Page {page.Number}, Image \"{img.Name}\": No alternative text.");
                    }
                }
            }
        }
    }
}