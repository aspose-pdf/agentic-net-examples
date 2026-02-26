using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputCgmPath  = "input.cgm";   // CGM input (input‑only format)
        const string outputPdfPath = "output.pdf";  // Resulting PDF with figure annotations

        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"File not found: {inputCgmPath}");
            return;
        }

        // Load the CGM file using the CGM load options (CGM is input‑only)
        CgmLoadOptions loadOptions = new CgmLoadOptions();

        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = new Document(inputCgmPath, loadOptions))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // ---- Add a Square (Figure) Annotation ----
            // Define the rectangle for the annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle squareRect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);
            SquareAnnotation square = new SquareAnnotation(page, squareRect);
            square.Color    = Aspose.Pdf.Color.Yellow;          // Use Aspose.Pdf.Color (cross‑platform)
            square.Contents = "Square Figure Annotation";
            page.Annotations.Add(square); // Add to the page's annotation collection

            // ---- Add a Circle (Figure) Annotation ----
            Aspose.Pdf.Rectangle circleRect = new Aspose.Pdf.Rectangle(300, 500, 400, 600);
            CircleAnnotation circle = new CircleAnnotation(page, circleRect);
            circle.Color    = Aspose.Pdf.Color.LightBlue;
            circle.Contents = "Circle Figure Annotation";
            page.Annotations.Add(circle);

            // Save the modified document as PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with figure annotations saved to '{outputPdfPath}'.");
    }
}