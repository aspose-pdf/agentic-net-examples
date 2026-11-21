using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // retained for completeness, not used in this example

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // source PDF
        const string outputSvgPath = "element.svg"; // SVG output (single page)
        const int graphicIndex = 2;                 // 1‑based index of the graphic element (page) to extract

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Validate the requested index.
                if (graphicIndex < 1 || graphicIndex > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Graphic index {graphicIndex} is out of range. Document has {pdfDoc.Pages.Count} pages.");
                    return;
                }

                // Create a temporary document that will contain only the requested page.
                using (Document singlePageDoc = new Document())
                {
                    // Add the selected page to the new document. The Add method copies the page content.
                    singlePageDoc.Pages.Add(pdfDoc.Pages[graphicIndex]);

                    // Configure SVG save options.
                    SvgSaveOptions svgOptions = new SvgSaveOptions
                    {
                        // Optional: enable CSS style embedding by scaling to pixels.
                        ScaleToPixels = true
                    };

                    // Save the single‑page document as SVG. When saving with SvgSaveOptions, one SVG file is created per page.
                    // Because the temporary document contains only one page, the result will be a single SVG file at the path provided.
                    singlePageDoc.Save(outputSvgPath, svgOptions);
                }

                Console.WriteLine($"Graphic element {graphicIndex} saved as SVG to '{outputSvgPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
