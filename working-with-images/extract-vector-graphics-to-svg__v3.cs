using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector; // GraphicsAbsorber lives here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const int targetPageNumber = 2; // 1‑based page index

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Validate page number
            if (targetPageNumber < 1 || targetPageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {pdfDoc.Pages.Count} pages.");
                return;
            }

            // Create a GraphicsAbsorber (core API, not Facades) and visit the desired page
            using (GraphicsAbsorber absorber = new GraphicsAbsorber())
            {
                Page page = pdfDoc.Pages[targetPageNumber];
                absorber.Visit(page); // extracts vector graphic elements from the page

                // Retrieve the extracted vector graphic elements
                // absorber.Elements returns a GraphicElementCollection, which implements IEnumerable<GraphicElement>
                // Use var or IEnumerable<GraphicElement> instead of IList<GraphicElement>
                var extractedGraphics = absorber.Elements;
                Console.WriteLine($"Extracted {extractedGraphics.Count} vector graphic(s) from page {targetPageNumber}.");

                // Example of further manipulation: save each graphic as a separate PDF page
                int graphicIndex = 1;
                foreach (GraphicElement graphic in extractedGraphics)
                {
                    // Create a new PDF document to host the graphic
                    using (Document graphicDoc = new Document())
                    {
                        // Add a blank page
                        Page newPage = graphicDoc.Pages.Add();

                        // Add the extracted graphic to the new page
                        // GraphicElement provides AddOnPage for this purpose
                        graphic.AddOnPage(newPage);

                        // Save the individual graphic PDF
                        string outputPath = $"graphic_page{targetPageNumber}_item{graphicIndex}.pdf";
                        graphicDoc.Save(outputPath);
                        Console.WriteLine($"Saved graphic #{graphicIndex} to '{outputPath}'.");
                    }
                    graphicIndex++;
                }
            }
        }
    }
}
