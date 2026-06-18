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

        using (Document doc = new Document(inputPath))
        {
            // Verify that page 6 exists (pages are 1‑based)
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The document has fewer than 6 pages.");
                return;
            }

            // Define the rectangle bounds (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a square (rectangle) annotation on page 6
            SquareAnnotation square = new SquareAnnotation(doc.Pages[6], rect);

            // Set the border color
            square.Color = Aspose.Pdf.Color.Blue;

            // Configure a dashed border and its width
            square.Border = new Border(square)
            {
                Width = 2,
                Dash = new Dash(new int[] { 3, 2 }) // 3 units dash, 2 units gap
            };

            // Set opacity to 50%
            square.Opacity = 0.5f;

            // Optional: add a tooltip or contents
            square.Contents = "Dashed rectangle annotation";

            // Add the annotation to the page
            doc.Pages[6].Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}