using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Vector;   // SvgExtractor and related vector types

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputSvgPath = "element_0.svg";      // target SVG file
        const int    elementIndex  = 0;                    // zero‑based index of the graphic element

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The document contains no pages.");
                return;
            }

            // Select the first page (adjust the index if another page is required)
            Page page = pdfDoc.Pages[1];

            // SvgExtractor extracts vector graphics from a page.
            // The Extract(Page) overload returns a list of SVG strings,
            // one entry per graphic element found on the page.
            SvgExtractor extractor = new SvgExtractor();
            List<string> svgElements = extractor.Extract(page);

            if (svgElements == null || svgElements.Count == 0)
            {
                Console.WriteLine("No vector graphic elements were found on the page.");
                return;
            }

            if (elementIndex < 0 || elementIndex >= svgElements.Count)
            {
                Console.Error.WriteLine($"Invalid element index. Page contains {svgElements.Count} elements.");
                return;
            }

            // Retrieve the SVG content of the requested element.
            string svgContent = svgElements[elementIndex];

            // Save the SVG string to a file.
            File.WriteAllText(outputSvgPath, svgContent);
            Console.WriteLine($"Graphic element #{elementIndex} saved as SVG to '{outputSvgPath}'.");
        }
    }
}