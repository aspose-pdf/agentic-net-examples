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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate over all images on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Retrieve the list of alternative texts for this image on the given page
                    List<string> altTexts = img.GetAlternativeText(page);

                    // If there are any alternative texts, display them
                    if (altTexts != null && altTexts.Count > 0)
                    {
                        Console.WriteLine($"Page {page.Number}, Image ID {img.Name}:");
                        foreach (string alt in altTexts)
                        {
                            Console.WriteLine($"  Alt Text: {alt}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Page {page.Number}, Image ID {img.Name}: No alternate text.");
                    }
                }
            }
        }
    }
}