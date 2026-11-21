using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Create a rectangle for the annotation (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                llx: 100,   // lower‑left X
                lly: 500,   // lower‑left Y
                urx: 300,   // upper‑right X
                ury: 550    // upper‑right Y
            );

            // Create the square (rectangle) annotation on the first page
            SquareAnnotation squareAnnot = new SquareAnnotation(doc.Pages[1], rect);

            // Set the border color (optional, default is black)
            squareAnnot.Color = Aspose.Pdf.Color.Black;

            // Set the fill (interior) color to light gray
            squareAnnot.InteriorColor = Aspose.Pdf.Color.LightGray;

            // Define the border width (1 pt) and style (solid)
            squareAnnot.Border = new Border(squareAnnot)
            {
                Width = 1,
                Style = BorderStyle.Solid
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(squareAnnot);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle annotation added and saved to '{outputPath}'.");
    }
}
