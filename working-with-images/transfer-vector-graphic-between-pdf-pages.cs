using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class TransferVectorGraphic
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"File not found: {sourcePdfPath}");
            return;
        }

        // Load the source PDF (lifecycle rule: wrap in using)
        using (Document srcDoc = new Document(sourcePdfPath))
        {
            // Assume we take the graphic from the first page
            Page srcPage = srcDoc.Pages[1];

            // Absorb vector graphics from the source page using the correct absorber
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(srcPage);

            // If no vector graphics were found, exit
            if (absorber.Elements == null || absorber.Elements.Count == 0)
            {
                Console.WriteLine("No vector graphics found on the source page.");
                srcDoc.Save(outputPdfPath); // Save original document unchanged
                return;
            }

            // Create a new PDF (or use an existing one) to host the transferred graphic
            using (Document destDoc = new Document())
            {
                // Add a blank page to the destination document
                Page destPage = destDoc.Pages.Add();

                // Transfer each extracted graphic element
                foreach (GraphicElement element in absorber.Elements)
                {
                    // Adjust position: set new coordinates (e.g., 100, 500)
                    element.Position = new Aspose.Pdf.Point(100, 500);

                    // Add the element to the destination page
                    element.AddOnPage(destPage);
                }

                // Save the result (lifecycle rule: use Document.Save)
                destDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Vector graphic transferred and saved to '{outputPdfPath}'.");
    }
}
