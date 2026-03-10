using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "figures_annotation.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Square (rectangle) figure annotation
            // -------------------------------------------------
            // Define the rectangle bounds (llx, lly, urx, ury) using the fully qualified type
            Aspose.Pdf.Rectangle squareRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
            // Create the square annotation on the page
            SquareAnnotation square = new SquareAnnotation(page, squareRect)
            {
                // Set visual properties
                Color = Aspose.Pdf.Color.Blue,          // Border color
                InteriorColor = Aspose.Pdf.Color.LightGray, // Fill color
                Title = "Square Figure",                // Title shown in the popup
                Contents = "This is a square figure annotation."
            };
            // Add the square annotation to the page's annotation collection
            page.Annotations.Add(square);

            // -------------------------------------------------
            // Circle figure annotation
            // -------------------------------------------------
            // Define the rectangle that bounds the circle
            Aspose.Pdf.Rectangle circleRect = new Aspose.Pdf.Rectangle(350, 500, 550, 700);
            // Create the circle annotation on the same page
            CircleAnnotation circle = new CircleAnnotation(page, circleRect)
            {
                Color = Aspose.Pdf.Color.Red,
                InteriorColor = Aspose.Pdf.Color.Yellow,
                Title = "Circle Figure",
                Contents = "This is a circle figure annotation."
            };
            // Add the circle annotation to the page
            page.Annotations.Add(circle);

            // Save the document to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with figure annotations saved to '{outputPath}'.");
    }
}