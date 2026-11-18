using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string headerImg  = "header.png";   // decorative header image
        const string outputPdf  = "output_with_header.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(headerImg))
        {
            Console.Error.WriteLine($"Header image not found: {headerImg}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create an ImageStamp for the header image
                ImageStamp headerStamp = new ImageStamp(headerImg)
                {
                    // Stretch the image to the full page width
                    Width  = page.PageInfo.Width,
                    // Desired height of the header (adjust as needed)
                    Height = 50,

                    // Position the stamp at the top of the page.
                    // Origin is bottom‑left, so Y = page height - header height - small gap.
                    XIndent = 0,
                    YIndent = page.PageInfo.Height - 50 - 5,

                    // Ensure the stamp does not affect existing page content.
                    // By placing it above the content area we avoid overlap.
                    // No background or border is needed.
                    Background = false,
                    Opacity    = 1.0
                };

                // Add the stamp to the current page
                page.AddStamp(headerStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Header image added to each page. Output saved as '{outputPdf}'.");
    }
}