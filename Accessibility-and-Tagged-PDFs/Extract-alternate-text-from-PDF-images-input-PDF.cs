using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class ExtractImageAltText
{
    static void Main(string[] args)
    {
        // Expect the input PDF path as the first argument.
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

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing).
            for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                Page page = pdfDocument.Pages[pageIndex];

                // The Images collection contains XImage objects defined in the page resources.
                foreach (XImage image in page.Resources.Images)
                {
                    // Retrieve alternative text for the image on the current page.
                    List<string> altTexts = image.GetAlternativeText(page);

                    // If no alternative text is present, indicate that.
                    if (altTexts == null || altTexts.Count == 0)
                    {
                        Console.WriteLine($"Page {pageIndex}, Image \"{image.Name}\": <no alternate text>");
                    }
                    else
                    {
                        // Concatenate multiple entries (if any) and display.
                        string combined = string.Join("; ", altTexts);
                        Console.WriteLine($"Page {pageIndex}, Image \"{image.Name}\": {combined}");
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