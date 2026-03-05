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

        // Open the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all XImage resources on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Retrieve the list of alternative texts for this image on this page
                    List<string> altTexts = img.GetAlternativeText(page);

                    // If any alternative text exists, output it
                    if (altTexts != null && altTexts.Count > 0)
                    {
                        Console.WriteLine($"Page {i}, Image Name: {img.Name}");
                        foreach (string txt in altTexts)
                        {
                            Console.WriteLine($"  Alt Text: {txt}");
                        }
                    }
                }
            }
        }
    }
}