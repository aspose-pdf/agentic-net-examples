using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // Facade namespace is included as requested

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

        // Load the PDF document (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Create a rectangle annotation (square annotation) on the first page
            // Fully qualified types are used to avoid ambiguity with System.Drawing
            Page firstPage = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // SquareAnnotation represents a rectangular annotation
            SquareAnnotation square = new SquareAnnotation(firstPage, rect)
            {
                // Set the border color (optional)
                Color = Aspose.Pdf.Color.Blue,
                // Set opacity to 75%
                Opacity = 0.75f,
                // Set contents (tooltip) – can be empty if not needed
                Contents = "Custom dashed rectangle"
            };

            // Configure the border: width and custom dash pattern
            // Border constructor requires the parent annotation instance
            square.Border = new Border(square)
            {
                Width = 2,                     // border width in points
                Dash = new Dash(new int[] { 5, 3 }) // dash pattern: 5 points on, 3 points off
            };

            // Add the annotation to the page
            firstPage.Annotations.Add(square);

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle annotation added and saved to '{outputPath}'.");
    }
}