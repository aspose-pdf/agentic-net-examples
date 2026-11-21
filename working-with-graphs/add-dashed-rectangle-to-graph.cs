using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // PDF that already contains the graph
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Assume the graph is on the first page
            Page page = doc.Pages[1];

            // Define rectangle coordinates (lower‑left x,y and upper‑right x,y)
            // Adjust these values to position the rectangle over the desired part of the graph
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 400);

            // Create a square annotation (represents a rectangular shape)
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                // Border colour
                Color = Aspose.Pdf.Color.Black,
                // No fill (transparent interior)
                InteriorColor = Aspose.Pdf.Color.Transparent
            };

            // Set a 2‑point dashed border – Border requires the parent annotation in the ctor
            square.Border = new Border(square)
            {
                Width = 2,
                Style = BorderStyle.Dashed
            };

            // Add the annotation to the page
            page.Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle added and saved to '{outputPath}'.");
    }
}
