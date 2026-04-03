using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string destPdfPath   = "destination.pdf";
        const string outputPath    = "merged.pdf";

        if (!File.Exists(sourcePdfPath) || !File.Exists(destPdfPath))
        {
            Console.Error.WriteLine("Source or destination PDF not found.");
            return;
        }

        using (Document srcDoc = new Document(sourcePdfPath))
        using (Document dstDoc = new Document(destPdfPath))
        {
            Page srcPage = srcDoc.Pages[1];
            Page dstPage = dstDoc.Pages[1];

            // Absorb vector graphics from the source page
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(srcPage);

            // Add each extracted graphic element to the destination page
            foreach (GraphicElement element in absorber.Elements)
            {
                element.AddOnPage(dstPage);
            }

            // Save the result
            dstDoc.Save(outputPath);
        }

        Console.WriteLine($"Vector graphics copied to '{outputPath}'.");
    }
}
