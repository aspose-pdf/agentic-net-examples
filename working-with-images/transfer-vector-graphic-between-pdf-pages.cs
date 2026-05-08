using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf  = "source.pdf";   // PDF containing the vector graphic
        const string outputPdf = "result.pdf";   // PDF with the graphic transferred
        const int  sourcePageNumber = 1;         // page to extract from (1‑based)
        const int  destPageNumber   = 2;         // page to place the graphic onto (1‑based)

        // Offsets to move the graphic on the destination page
        const float offsetX = 100f; // move right by 100 points
        const float offsetY = 50f;  // move up by 50 points

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (core API)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the destination page exists; add a blank page if necessary
            while (doc.Pages.Count < Math.Max(sourcePageNumber, destPageNumber))
                doc.Pages.Add();

            Page sourcePage = doc.Pages[sourcePageNumber];
            Page destPage   = doc.Pages[destPageNumber];

            // Extract vector graphics from the source page
            using (GraphicsAbsorber absorber = new GraphicsAbsorber())
            {
                // NOTE: GraphicsAbsorber uses Visit(page) – not Page.Accept
                absorber.Visit(sourcePage);

                // If no vector graphics were found, exit
                if (absorber.Elements.Count == 0)
                {
                    Console.WriteLine("No vector graphics found on the source page.");
                }
                else
                {
                    // Transfer each extracted graphic to the destination page
                    foreach (GraphicElement element in absorber.Elements)
                    {
                        // Adjust position by setting the Position property.
                        // This updates the internal transformation matrix.
                        // Position expects an Aspose.Pdf.Point (X, Y) in user space.
                        element.Position = new Aspose.Pdf.Point(
                            element.Position.X + offsetX,
                            element.Position.Y + offsetY);

                        // Add the element to the destination page.
                        // AddOnPage works for a single element; for many elements AddGraphics is faster.
                        element.AddOnPage(destPage);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Vector graphic transferred and saved to '{outputPdf}'.");
    }
}