using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document to obtain a Page object (required for TextAnnotation ctor)
        Document doc = new Document(inputPath);
        Page page = doc.Pages[1];

        // Initialize the annotation editor facade
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the same PDF file to the editor
            editor.BindPdf(inputPath);

            // Create an annotation with the ReadOnly flag using the (Page, Rectangle) constructor
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
            TextAnnotation annot = new TextAnnotation(page, rect)
            {
                Flags    = AnnotationFlags.ReadOnly, // Apply read‑only flag before ModifyAnnotations
                Title    = "ReadOnly",
                Contents = "This annotation is read‑only",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true
            };

            // Modify annotations on the desired page range (here page 1 only)
            editor.ModifyAnnotations(1, 1, annot);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
