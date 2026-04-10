using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

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

        // Load the document to obtain a Page object – required for TextAnnotation construction.
        Document doc = new Document(inputPath);
        Page page = doc.Pages[1]; // target page (1‑based index)

        // Create a new TextAnnotation using the (Page, Rectangle) constructor.
        // The rectangle defines the annotation's position on the page.
        TextAnnotation newAnnotation = new TextAnnotation(page, new Aspose.Pdf.Rectangle(100, 700, 200, 750))
        {
            Subject = "Updated Subject",
            Color   = Aspose.Pdf.Color.Blue
        };

        // Bind the PDF to the annotation editor.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Replace annotations on page 1 (start = end = 1) with the new annotation.
            editor.ModifyAnnotations(start: 1, end: 1, annotation: newAnnotation);

            // Save the updated PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated annotation to '{outputPath}'.");
    }
}
