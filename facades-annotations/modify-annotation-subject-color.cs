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

        // Load the document to obtain a Page instance – required for the TextAnnotation constructor.
        Document doc = new Document(inputPath);
        Page page = doc.Pages[1];

        // TextAnnotation does not have a parameter‑less constructor. Use the (Page, Rectangle) overload.
        // A zero‑size rectangle is sufficient when the annotation is only used as a template for modification.
        TextAnnotation newAnnotation = new TextAnnotation(page, new Aspose.Pdf.Rectangle(0, 0, 0, 0))
        {
            Subject = "Updated Subject",
            Color   = Aspose.Pdf.Color.Blue
        };

        // Initialize the annotation editor facade and bind the PDF.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Apply the changes to annotations on page 1 (start = end = 1).
        editor.ModifyAnnotations(1, 1, newAnnotation);

        // Save the modified PDF.
        editor.Save(outputPath);
    }
}
