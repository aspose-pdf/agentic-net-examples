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

        // Load the document and bind it to the annotation editor
        using (Document doc = new Document(inputPath))
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(doc);

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text annotation on page 5 (pages are 1‑based)
            TextAnnotation annotation = new TextAnnotation(doc.Pages[5], rect)
            {
                Title = "Alice",          // author of the annotation
                Contents = "Sample annotation"
            };

            // Add the annotation to page 5 using ModifyAnnotations
            editor.ModifyAnnotations(5, 5, annotation);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}
