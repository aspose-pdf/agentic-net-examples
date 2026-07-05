using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector; // GraphicsAbsorber and GraphicElement

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string outputPdfPath = "output.pdf";

        // Page numbers are 1‑based in Aspose.Pdf
        const int sourcePageNumber = 1;
        const int destinationPageNumber = 2;

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(sourcePdfPath))
        {
            // Ensure the destination page exists
            if (doc.Pages.Count < destinationPageNumber)
                doc.Pages.Add();

            Page sourcePage = doc.Pages[sourcePageNumber];
            Page destinationPage = doc.Pages[destinationPageNumber];

            // Extract vector graphics from the source page
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(sourcePage);

            // Transfer the first extracted graphic element, if any
            if (absorber.Elements.Count > 0)
            {
                // The collection holds GraphicElement objects (e.g., SubPath)
                GraphicElement graphic = absorber.Elements[0];

                // Adjust its position on the destination page.
                // Position expects a Point (X, Y) in user units.
                graphic.Position = new Point(100, 200);

                // Add the graphic element to the destination page.
                graphic.AddOnPage(destinationPage);
            }
            else
            {
                Console.WriteLine("No vector graphics found on the source page.");
            }

            // Save the modified document (lifecycle rule: use Document.Save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Vector graphic transferred and saved to '{outputPdfPath}'.");
    }
}
