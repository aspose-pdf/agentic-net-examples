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

        // Load the document to obtain a Page object required for the TextAnnotation constructor
        Document doc = new Document(inputPath);
        Page page = doc.Pages[1]; // target page (1‑based index)

        // Create a TextAnnotation using the (Page, Rectangle) constructor.
        // An empty rectangle is sufficient when we only want to modify existing annotation properties.
        TextAnnotation newAnnotation = new TextAnnotation(page, new Aspose.Pdf.Rectangle(0, 0, 0, 0))
        {
            Subject = "Updated Subject",
            Color   = Aspose.Pdf.Color.Red
        };

        // Bind the PDF to the annotation editor
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Apply the changes to annotations on the target page range (page 1)
        editor.ModifyAnnotations(start: 1, end: 1, annotation: newAnnotation);

        // Save the modified PDF and release resources
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Annotation subject and color updated. Saved to '{outputPath}'.");
    }
}
