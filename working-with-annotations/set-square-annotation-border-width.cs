using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a square figure annotation on the first page
            SquareAnnotation square = new SquareAnnotation(doc.Pages[1], rect);

            // Set the border width to 2 points for clearer visualization
            square.Border = new Border(square) { Width = 2 };

            // Optional: set a visible color for the border
            square.Color = Aspose.Pdf.Color.Blue;

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure annotation with 2‑point line thickness saved to '{outputPath}'.");
    }
}