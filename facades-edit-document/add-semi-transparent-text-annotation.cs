using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes (Document, Rectangle, etc.)
using Aspose.Pdf.Annotations;         // Annotation types (e.g., TextAnnotation)
using Aspose.Pdf.Facades;             // Facade API (optional, not needed for this simple case)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // result PDF with transparent annotation

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Load the PDF document.
        // -----------------------------------------------------------------
        Document doc = new Document(inputPdf);

        // -----------------------------------------------------------------
        // Define the rectangle where the annotation will appear.
        // Fully qualify to avoid ambiguity with System.Drawing.Rectangle.
        // -----------------------------------------------------------------
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // -----------------------------------------------------------------
        // Create a TextAnnotation (a type of MarkupAnnotation) and set its
        // opacity to 0.5 (50 % transparent).
        // The constructor that accepts a Page and a Rectangle must be used.
        // -----------------------------------------------------------------
        TextAnnotation textAnn = new TextAnnotation(doc.Pages[1], rect)
        {
            Title    = "Note",
            Contents = "This annotation is semi‑transparent.",
            Color    = Aspose.Pdf.Color.Yellow,
            Opacity  = 0.5   // 0 = fully transparent, 1 = fully opaque
        };

        // -----------------------------------------------------------------
        // Add the annotation to the first page and save the modified PDF.
        // -----------------------------------------------------------------
        doc.Pages[1].Annotations.Add(textAnn);
        doc.Save(outputPdf);

        Console.WriteLine($"Annotation with 50 % opacity saved to '{outputPdf}'.");
    }
}
