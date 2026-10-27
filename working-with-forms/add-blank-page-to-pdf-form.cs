using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_form.pdf";
        const string outputPath = "output_form_with_blank_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF form. Using block ensures deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document.
            // The new page inherits default size; we optionally match the size of the first page.
            Page blankPage = doc.Pages.Add();

            // If the document already has pages, copy the size of the first page to keep consistency.
            if (doc.Pages.Count > 1) // doc.Pages[1] is the first page (1‑based indexing)
            {
                Page firstPage = doc.Pages[1];
                blankPage.PageInfo.Width  = firstPage.PageInfo.Width;
                blankPage.PageInfo.Height = firstPage.PageInfo.Height;
            }

            // Existing form fields remain untouched; no additional handling required.

            // Save the modified PDF. Since the extension is .pdf, Document.Save writes PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page added successfully. Output saved to '{outputPath}'.");
    }
}