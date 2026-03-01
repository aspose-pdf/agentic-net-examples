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
            // Store alternate texts per page (page number -> list of alt texts)
            var imageAlts = new Dictionary<int, List<string>>();

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                var altsForPage = new List<string>();

                // XImageCollection is iterated directly, not as a dictionary
                foreach (XImage img in page.Resources.Images)
                {
                    // GetAlternativeText returns a List<string> for the given page
                    List<string> altTexts = img.GetAlternativeText(page);
                    if (altTexts != null && altTexts.Count > 0)
                    {
                        altsForPage.AddRange(altTexts);
                    }
                }

                if (altsForPage.Count > 0)
                {
                    imageAlts[i] = altsForPage;
                }
            }

            // Output the collected alternate texts
            foreach (var kvp in imageAlts)
            {
                Console.WriteLine($"Page {kvp.Key}:");
                foreach (string alt in kvp.Value)
                {
                    Console.WriteLine($"  Alt Text: {alt}");
                }
            }
        }
    }
}