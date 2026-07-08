using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_with_header.pdf"; // result PDF
        const string headerImg = "header.png";         // decorative header image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(headerImg))
        {
            Console.Error.WriteLine($"Header image not found: {headerImg}");
            return;
        }

        // Load the document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate pages using 1‑based indexing (rule: page-indexing-one-based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define header image size and position.
                // Width and height are chosen to fit typical header space.
                double imgWidth  = 120;   // points
                double imgHeight = 60;    // points
                double margin    = 10;    // space from page edges

                // Position the image at the top‑left corner, respecting the margin.
                // Aspose.Pdf.Rectangle constructor: (llx, lly, urx, ury)
                double llx = margin;
                double lly = page.PageInfo.Height - imgHeight - margin;
                double urx = llx + imgWidth;
                double ury = page.PageInfo.Height - margin;

                // Fully qualified Rectangle to avoid ambiguity (rule: rectangle-disambiguation)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Add the image to the page. This method places the image inside the rectangle
                // without scaling beyond the rectangle bounds, preserving aspect ratio.
                page.AddImage(headerImg, rect);
            }

            // Save the modified document (rule: create‑load‑save lifecycle)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Header image added to each page. Saved as '{outputPdf}'.");
    }
}