using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Absorb all vector graphics on the page
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(page);

            // Remove a specific vector graphic (e.g., the first one)
            if (absorber.Elements.Count > 0)
            {
                // Each element is a GraphicElement; call Remove() to delete it
                absorber.Elements[0].Remove();
                Console.WriteLine("Removed a vector graphic from the page.");
            }
            else
            {
                Console.WriteLine("No vector graphics found on the page.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
