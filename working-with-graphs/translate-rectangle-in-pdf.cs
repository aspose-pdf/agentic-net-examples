using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the original rectangle (lower‑left X,Y and upper‑right X,Y)
            // Fully qualify the type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Translate the rectangle: shift 50 units right (dx) and 30 units down (dy)
            rect.MoveBy(50, -30); // Horizontal shift = +50, Vertical shift = -30

            // Visualise the moved rectangle using a SquareAnnotation
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Blue   // Set border color
            };
            page.Annotations.Add(square);

            // Save the PDF (PDF format is the default)
            doc.Save("translated.pdf");
        }

        Console.WriteLine("PDF with translated rectangle saved as 'translated.pdf'.");
    }
}