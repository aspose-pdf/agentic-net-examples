using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;          // Figure annotations (SquareAnnotation, CircleAnnotation)
using Aspose.Pdf.Drawing;             // For Aspose.Pdf.Rectangle (fully qualified if needed)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputXps = "output.xps";     // XPS output

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // === Document lifecycle: load, modify, save (using blocks) ===
        using (Document doc = new Document(inputPdf))
        {
            // Choose the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define a rectangle for the figure annotation (llx, lly, urx, ury)
            // Use fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a square figure annotation on the page
            SquareAnnotation square = new SquareAnnotation(page, rect);
            square.Color = Aspose.Pdf.Color.Blue;          // border color
            square.InteriorColor = Aspose.Pdf.Color.LightGray; // fill color
            square.Opacity = 0.5;                           // semi‑transparent
            square.Contents = "Sample square annotation";

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(square);

            // Optionally, add a circle annotation as another example
            CircleAnnotation circle = new CircleAnnotation(page, rect);
            circle.Color = Aspose.Pdf.Color.Red;
            circle.InteriorColor = Aspose.Pdf.Color.Yellow;
            circle.Opacity = 0.4;
            circle.Contents = "Sample circle annotation";
            page.Annotations.Add(circle);

            // Save the modified document as XPS.
            // Document.Save(string) without SaveOptions always writes PDF,
            // so we must pass an explicit XpsSaveOptions instance.
            XpsSaveOptions xpsOpts = new XpsSaveOptions();
            doc.Save(outputXps, xpsOpts);
        }

        Console.WriteLine($"Annotations added and document saved as XPS: '{outputXps}'.");
    }
}