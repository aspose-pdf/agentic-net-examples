using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // needed for Color

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
        Document pdfDoc = new Document(inputPath);
        Page firstPage = pdfDoc.Pages[1];

        // Define the rectangle that will bound the annotation (lower‑left x,y and upper‑right x,y)
        var annotRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

        // Create a SquareAnnotation (rectangle) on the first page
        SquareAnnotation rectAnnot = new SquareAnnotation(firstPage, annotRect)
        {
            // Border (stroke) color
            Color = Color.Blue,

            // Fill (interior) color
            InteriorColor = Color.LightGray,

            // 75% opacity
            Opacity = 0.75f
        };

        // Configure the border: width, style and custom dash pattern
        rectAnnot.Border = new Border(rectAnnot)
        {
            // Width expects an integer
            Width = 1,
            Style = BorderStyle.Solid,
            // Dash pattern: 3 units on, 2 units off (Dash object expects an int[])
            Dash = new Dash(new int[] { 3, 2 })
        };

        // Add the annotation to the first page
        firstPage.Annotations.Add(rectAnnot);

        // Save the modified PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Rectangle annotation added to page 1 and saved as '{outputPath}'.");
    }
}
