using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for Matrix
using Aspose.Pdf.Vector; // for GraphicsAbsorber and GraphicElement

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string destinationPath = "destination.pdf";

        // Page numbers are 1‑based in Aspose.Pdf
        const int sourcePageNumber = 1;
        const int destinationPageNumber = 2;

        // Desired translation of the graphic on the destination page
        const double offsetX = 100.0; // move right
        const double offsetY = 50.0;  // move up

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        // Load the source PDF (the same document is used as destination for simplicity)
        using (Document srcDoc = new Document(sourcePath))
        using (Document dstDoc = new Document(sourcePath))
        {
            Page srcPage = srcDoc.Pages[sourcePageNumber];
            Page dstPage = dstDoc.Pages[destinationPageNumber];

            // Extract vector graphics from the source page
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(srcPage);

            // Translation matrix – moves the graphic by (offsetX, offsetY)
            Matrix translation = new Matrix(1, 0, 0, 1, offsetX, offsetY);

            // Iterate over each extracted graphic element
            foreach (GraphicElement element in absorber.Elements)
            {
                // Adjust the element's position using the translation matrix.
                // If the element exposes a Matrix property we could multiply it, but the
                // safest way across Aspose.Pdf versions is to shift the Position.
                Point currentPos = element.Position;
                element.Position = new Point(currentPos.X + offsetX, currentPos.Y + offsetY);

                // Alternatively, when Matrix is available you could use:
                // element.Matrix = element.Matrix * translation;

                // Place the modified element onto the destination page.
                element.AddOnPage(dstPage);
            }

            // Save the modified document
            dstDoc.Save(destinationPath);
        }

        Console.WriteLine($"Vector graphic transferred and saved to '{destinationPath}'.");
    }
}
