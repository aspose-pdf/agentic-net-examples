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
        const string outputPath = "output_custom_flags.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to obtain the total number of pages (required for ModifyAnnotations range)
        int pageCount;
        Document doc = new Document(inputPath);
        pageCount = doc.Pages.Count; // 1‑based page count

        // Get a reference page (any page can be used for the constructor)
        Page firstPage = doc.Pages[1];

        // Create a concrete annotation (TextAnnotation) using the (Page, Rectangle) constructor
        TextAnnotation customAnnotation = new TextAnnotation(
            firstPage,
            new Aspose.Pdf.Rectangle(100, 500, 300, 550)
        )
        {
            Title = "Custom Flag",
            Contents = "Annotation with custom flags",
            Color = Aspose.Pdf.Color.Yellow,
            Flags = AnnotationFlags.Invisible | AnnotationFlags.NoZoom
        };

        // Use PdfAnnotationEditor (Facades API) to apply the modified annotation to the document
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);                     // Load the PDF into the editor
            editor.ModifyAnnotations(1, pageCount, customAnnotation); // Apply to all pages
            editor.Save(outputPath);                       // Persist changes
        }

        Console.WriteLine($"PDF saved with custom annotation flags: '{outputPath}'.");
    }
}