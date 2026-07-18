using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // for Color
using Aspose.Pdf.Text;   // for BorderStyle enum (in Aspose.Pdf.Annotations)

class Program
{
    static void Main()
    {
        const string outputPath = "dashed_rectangle_annotation.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle area for the annotation (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a square (rectangle) annotation on the page
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                // Optional: set a background color to make the annotation visible
                Color = Aspose.Pdf.Color.LightGray,
                // Optional: set contents (tooltip) for the annotation
                Contents = "Dashed border rectangle"
            };

            // Configure the border: set style to Dashed and define dash pattern
            Border border = new Border(square);
            border.Style = BorderStyle.Dashed;          // Dashed border style
            border.Width = 2;                           // Border width in points
            border.Dash = new Dash(5, 3);               // 5 points dash, 3 points gap
            square.Border = border;                     // Assign the configured border to the annotation

            // Add the annotation to the page
            page.Annotations.Add(square);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dashed rectangle annotation saved to '{outputPath}'.");
    }
}