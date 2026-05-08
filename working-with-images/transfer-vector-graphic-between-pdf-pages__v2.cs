using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string outputPdfPath = "result.pdf";

        // Verify input files exist
        if (!File.Exists(sourcePdfPath) || !File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine("Source or target PDF file not found.");
            return;
        }

        // Load source and target documents inside using blocks for deterministic disposal
        using (Document sourceDoc = new Document(sourcePdfPath))
        using (Document targetDoc = new Document(targetPdfPath))
        {
            // Choose pages to work with (first page in this example)
            Page sourcePage = sourceDoc.Pages[1];
            Page targetPage = targetDoc.Pages[1];

            // Extract vector graphics from the source page using the correct absorber class
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(sourcePage);

            // Add each extracted graphic element to the target page.
            // GraphicsAbsorber.Elements returns a collection of GraphicElement objects.
            foreach (var graphic in absorber.Elements)
            {
                graphic.AddOnPage(targetPage);
            }

            // Save the modified target document
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Vector graphic transferred and saved to '{outputPdfPath}'.");
    }
}
