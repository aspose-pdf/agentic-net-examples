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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate over all XImage resources on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Retrieve the list of alternative texts for this image on the page
                    List<string> altTexts = img.GetAlternativeText(page);

                    // If there are any alternative texts, output them
                    if (altTexts != null && altTexts.Count > 0)
                    {
                        foreach (string alt in altTexts)
                        {
                            Console.WriteLine($"Page {page.Number}: Image Alt Text = \"{alt}\"");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Page {page.Number}: Image has no alternative text.");
                    }
                }
            }
        }
    }
}