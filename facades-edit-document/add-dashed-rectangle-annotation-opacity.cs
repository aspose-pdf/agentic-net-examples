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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle area for the annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a square (rectangle) annotation
            SquareAnnotation rectAnnot = new SquareAnnotation(page, rect)
            {
                // Set the border color
                Color = Aspose.Pdf.Color.Red,
                // Set opacity to 75%
                Opacity = 0.75f,
                // Optional: add some contents (tooltip)
                Contents = "Custom dashed rectangle"
            };

            // Configure the border with custom dash pattern
            // Dash pattern: 3 units on, 2 units off
            rectAnnot.Border = new Border(rectAnnot)
            {
                Width = 2,
                Dash = new Dash(new int[] { 3, 2 })
            };

            // Add the annotation to the page
            page.Annotations.Add(rectAnnot);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle annotation added and saved to '{outputPath}'.");
    }
}