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
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the figure annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a square (figure) annotation on the page
            SquareAnnotation square = new SquareAnnotation(page, rect);

            // Set the border width to 2 points (must use Border ctor with the annotation)
            square.Border = new Border(square) { Width = 2 };

            // Optional: set a visible color for the border
            square.Color = Aspose.Pdf.Color.Blue;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(square);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}