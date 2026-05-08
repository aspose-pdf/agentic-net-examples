using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the figure annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a square (figure) annotation on the page
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Yellow, // Border color
                Contents = "Sample square annotation"
            };

            // Set the line thickness (border width) to 2 points
            square.Border = new Border(square) { Width = 2 };

            // Add the annotation to the page
            page.Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}