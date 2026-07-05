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
            // Verify that page 6 exists (1‑based indexing)
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The document has fewer than 6 pages.");
                return;
            }

            Page page = doc.Pages[6];

            // Define the rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a square (rectangle) annotation
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                // Optional contents displayed when the annotation is selected
                Contents = "Dashed rectangle annotation",
                // Set 50 % opacity
                Opacity = 0.5,
                // Border color (the annotation's own Color property controls border color)
                Color = Aspose.Pdf.Color.Red
            };

            // Configure the border: dashed style and width
            square.Border = new Border(square)
            {
                Width = 2,
                // Dash pattern: 3 units on, 3 units off
                Dash = new Dash(new int[] { 3, 3 })
            };

            // Add the annotation to the page
            page.Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle annotation added and saved to '{outputPath}'.");
    }
}
