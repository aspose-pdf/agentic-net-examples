using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector; // GraphicsAbsorber and GraphicElement

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
            // Choose the page that contains the vector graphic (1‑based index)
            Page page = doc.Pages[1];

            // Absorb all vector graphics on the page
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(page);

            // Locate the specific vector graphic to delete.
            // Here we simply delete the first one; replace this logic
            // with any condition that identifies the target graphic.
            if (absorber.Elements.Count > 0)
            {
                // Remove the graphic from the page
                absorber.Elements[0].Remove();
            }

            // Save the modified PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
