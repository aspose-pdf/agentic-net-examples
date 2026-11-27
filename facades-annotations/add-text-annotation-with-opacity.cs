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

        // Load the PDF document
        Document doc = new Document(inputPath);
        // Get the first page (page numbers are 1‑based)
        Page page = doc.Pages[1];

        // Define the annotation rectangle
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create a TextAnnotation and set its properties, including opacity
        TextAnnotation annotation = new TextAnnotation(page, rect)
        {
            Title    = "Note",
            Contents = "Annotation with 0.75 opacity",
            Open     = true,
            Icon     = TextIcon.Note,
            Opacity  = 0.75f // opacity value (0.0 to 1.0)
        };

        // Add the annotation to the page's annotation collection
        page.Annotations.Add(annotation);

        // Save the modified PDF using PdfAnnotationEditor (or directly via Document)
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(doc);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
