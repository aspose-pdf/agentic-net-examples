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

        // Load the PDF document (1‑based page indexing)
        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all XImage resources on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Retrieve the list of alternative texts for this image on this page
                    List<string> altTexts = img.GetAlternativeText(page);

                    if (altTexts != null && altTexts.Count > 0)
                    {
                        foreach (string alt in altTexts)
                        {
                            // Output the alternative text (image name may be null, so we guard)
                            string imgName = string.IsNullOrEmpty(img.Name) ? "(unnamed)" : img.Name;
                            Console.WriteLine($"Page {i}, Image \"{imgName}\": Alt Text = \"{alt}\"");
                        }
                    }
                    else
                    {
                        string imgName = string.IsNullOrEmpty(img.Name) ? "(unnamed)" : img.Name;
                        Console.WriteLine($"Page {i}, Image \"{imgName}\": No alternative text.");
                    }
                }
            }
        }
    }
}