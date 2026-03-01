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

        // Load the PDF document (lifecycle: create, load, save)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                int imageIndex = 1;

                // XImageCollection is enumerable; iterate directly
                foreach (XImage img in page.Resources.Images)
                {
                    // Retrieve alternate text(s) for the current image on this page
                    List<string> altTexts = img.GetAlternativeText(page);

                    Console.WriteLine($"Page {page.Number}, Image #{imageIndex}:");

                    if (altTexts != null && altTexts.Count > 0)
                    {
                        foreach (string alt in altTexts)
                        {
                            Console.WriteLine($"  Alt text: {alt}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  No alternate text.");
                    }

                    imageIndex++;
                }
            }
        }
    }
}