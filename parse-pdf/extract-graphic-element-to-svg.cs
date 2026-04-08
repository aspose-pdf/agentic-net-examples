using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputSvg = "element_3.svg";      // SVG file for the extracted element
        const int    elementIndex = 3;                 // 1‑based index of the graphic element to extract

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (using block ensures proper disposal)
            using (Document doc = new Document(inputPdf))
            {
                // Choose the page from which to extract graphics (first page in this example)
                Page page = doc.Pages[1]; // pages are 1‑based

                // Absorb all graphic elements on the page
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    absorber.Visit(page); // populate absorber.Elements

                    // Ensure the requested index exists
                    if (elementIndex < 1 || elementIndex > absorber.Elements.Count)
                    {
                        Console.Error.WriteLine($"Element index {elementIndex} is out of range (1‑{absorber.Elements.Count}).");
                        return;
                    }

                    // Retrieve the element (zero‑based collection, so subtract 1)
                    GraphicElement element = absorber.Elements.ElementAt(elementIndex - 1);

                    // Save the selected graphic element as an SVG file
                    element.SaveToSvg(outputSvg);
                }
            }

            Console.WriteLine($"Graphic element #{elementIndex} saved as SVG to '{outputSvg}'.");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}