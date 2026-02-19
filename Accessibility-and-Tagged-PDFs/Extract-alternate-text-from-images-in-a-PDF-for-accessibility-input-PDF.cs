using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class ExtractImageAltText
{
    static void Main(string[] args)
    {
        // Expect the PDF path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: ExtractImageAltText <input-pdf-path>");
            return;
        }

        string pdfPath = args[0];

        // Verify that the file exists before attempting to load it.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(pdfPath);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing).
            for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                Page page = pdfDocument.Pages[pageIndex];

                // Each page may contain a collection of XImage objects.
                // The Resources.Images collection holds all images referenced on the page.
                foreach (XImage image in page.Resources.Images)
                {
                    // Retrieve alternative text (alt) for the image on this page.
                    // The method returns a list of strings; usually the first entry is the alt text.
                    IList<string> altTexts = image.GetAlternativeText(page);

                    // If no alternative text is defined, indicate that.
                    if (altTexts == null || altTexts.Count == 0)
                    {
                        Console.WriteLine($"Page {pageIndex}: Image \"{image.Name}\" – No alternate text.");
                    }
                    else
                    {
                        // Concatenate multiple entries if present.
                        string combinedAlt = string.Join("; ", altTexts);
                        Console.WriteLine($"Page {pageIndex}: Image \"{image.Name}\" – Alt text: {combinedAlt}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }
}