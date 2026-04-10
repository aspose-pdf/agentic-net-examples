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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over each XImage on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Retrieve alternative text(s) for the image on this page
                    List<string> altTexts = img.GetAlternativeText(page);

                    // Log the results; if none, indicate that no alt text is set
                    if (altTexts != null && altTexts.Count > 0)
                    {
                        foreach (string alt in altTexts)
                        {
                            Console.WriteLine($"Page {i}, Image \"{img.Name}\": Alt Text = \"{alt}\"");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Page {i}, Image \"{img.Name}\": No alternative text set.");
                    }
                }
            }
        }
    }
}