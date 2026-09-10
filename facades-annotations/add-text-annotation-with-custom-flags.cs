using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the annotation editor
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Get a reference page (any page will do – we use the first one for the constructor)
        Page referencePage = editor.Document.Pages[1];
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create a new text annotation using the (Page, Rectangle) constructor
        TextAnnotation annotation = new TextAnnotation(referencePage, rect)
        {
            Title    = "Custom Flag",
            Contents = "Annotation with custom flags",
            Color    = Aspose.Pdf.Color.Yellow,
            // Combine custom flags (e.g., Invisible and NoZoom)
            Flags    = AnnotationFlags.Invisible | AnnotationFlags.NoZoom
        };

        // Apply the annotation to all pages (1‑based indexing)
        editor.ModifyAnnotations(1, editor.Document.Pages.Count, annotation);

        // Save the modified document
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
