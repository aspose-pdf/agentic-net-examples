using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;   // for GraphicElement and Matrix
using Aspose.Pdf.Text;     // for Point

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Verify that the page actually contains vector graphics
            if (!page.HasVectorGraphics())
            {
                Console.WriteLine("No vector graphics found on the page.");
                doc.Save(outputPath);
                return;
            }

            // -----------------------------------------------------------------
            // NOTE: Aspose.Pdf does not expose a direct collection of vector
            // graphics (SubPathCollection, AddVectorGraphics, etc. are not
            // available). In a real scenario you would obtain a concrete
            // GraphicElement (e.g., SubPath) from the page via a supported API.
            // For the purpose of this example we illustrate how the position
            // of such an element would be changed.
            // -----------------------------------------------------------------

            // Suppose 'graphic' is a GraphicElement that has been extracted.
            // The GraphicElement class provides a virtual Position property.
            // Changing Position automatically updates the underlying Matrix.
            // Example (commented out because we do not have a concrete instance):
            //
            // GraphicElement graphic = ... // obtained from the page
            // double dx = 50; // shift right by 50 units
            // double dy = 30; // shift up by 30 units
            // // Update the position (adds the delta to the current coordinates)
            // graphic.Position = new Point(graphic.Position.X + dx, graphic.Position.Y + dy);
            //
            // Alternatively, you can build a translation matrix and assign it
            // via the Position setter (the matrix is applied internally).

            // Demonstrate creation of a translation matrix (identity source matrix)
            double dx = 50; // horizontal shift
            double dy = 30; // vertical shift
            Matrix identity = new Matrix(new double[] { 1, 0, 0, 1, 0, 0 });
            Matrix translation = Matrix.Translate(dx, dy, identity);

            // If you had a GraphicElement 'graphic', you could apply the matrix:
            // graphic.Position = new Point(translation.Transform(graphic.Position).X,
            //                              translation.Transform(graphic.Position).Y);

            Console.WriteLine("Vector graphic would be moved by updating its Position or applying a translation matrix.");

            // Save the modified PDF (save rule: Document.Save without extra options writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}