using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input CGM file and output PDF paths
        const string cgmPath   = "input.cgm";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        // Load the CGM file into a PDF document using CgmLoadOptions (input‑only format)
        using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // -------------------------------------------------
            // Add a CircleAnnotation (a figure annotation)
            // -------------------------------------------------
            // Define the rectangle that bounds the circle
            Aspose.Pdf.Rectangle circleRect = new Aspose.Pdf.Rectangle(100, 600, 200, 700);
            // Create the circle annotation on the page
            CircleAnnotation circle = new CircleAnnotation(page, circleRect)
            {
                Color   = Aspose.Pdf.Color.Blue,          // Border color
                InteriorColor = Aspose.Pdf.Color.LightBlue // Fill color
            };
            // Add the annotation to the page's annotation collection
            page.Annotations.Add(circle);

            // -------------------------------------------------
            // Add a SquareAnnotation (another figure annotation)
            // -------------------------------------------------
            Aspose.Pdf.Rectangle squareRect = new Aspose.Pdf.Rectangle(250, 600, 350, 700);
            SquareAnnotation square = new SquareAnnotation(page, squareRect)
            {
                Color   = Aspose.Pdf.Color.Green,
                InteriorColor = Aspose.Pdf.Color.LightGreen
            };
            page.Annotations.Add(square);

            // -------------------------------------------------
            // Save the resulting PDF
            // -------------------------------------------------
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with figure annotations saved to '{pdfPath}'.");
    }
}