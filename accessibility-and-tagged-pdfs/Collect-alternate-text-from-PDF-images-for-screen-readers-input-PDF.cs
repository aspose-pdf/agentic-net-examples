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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                Console.WriteLine($"Page {i}:");

                // Iterate over all XImage resources on the page
                foreach (XImage img in page.Resources.Images)
                {
                    // Retrieve the list of alternative texts for this image on the current page
                    List<string> altTexts = img.GetAlternativeText(page);

                    if (altTexts != null && altTexts.Count > 0)
                    {
                        foreach (string alt in altTexts)
                        {
                            Console.WriteLine($"  Image Alt Text: \"{alt}\"");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  Image has no alternative text.");
                    }
                }
            }
        }
    }
}