using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Path to the input PDF
        const string inputPath = "input.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDocument.Pages.Count; pageNum++)
            {
                Page page = pdfDocument.Pages[pageNum];

                // Each page may have images defined in its resources collection
                foreach (XImage image in page.Resources.Images)
                {
                    // Retrieve alternate text for the image on the current page
                    IList<string> altTexts = image.GetAlternativeText(page);

                    if (altTexts != null && altTexts.Count > 0)
                    {
                        Console.WriteLine($"Page {pageNum}, Image \"{image.Name}\":");
                        foreach (string txt in altTexts)
                        {
                            Console.WriteLine($"  Alt Text: {txt}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}