using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "updated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the annotation editor facade and bind the PDF.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // A TextAnnotation cannot be instantiated with a parameter‑less constructor.
        // Use the (Page, Rectangle) constructor. The rectangle values are ignored by
        // ModifyAnnotations – they are only required for object creation.
        Page anyPage = editor.Document.Pages[1];
        // Fully qualify the Rectangle type to avoid ambiguity with Aspose.Pdf.Drawing.Rectangle.
        Aspose.Pdf.Rectangle dummyRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
        TextAnnotation updatedAnnotation = new TextAnnotation(anyPage, dummyRect)
        {
            Modified = DateTime.Now,                     // Update modification date.
            Title    = "Updated Author",                // New author/title.
            Contents = "Updated annotation contents.", // New popup text.
            Color    = Aspose.Pdf.Color.Red,            // Change annotation colour.
            Subject  = "Updated Subject",               // Optional subject.
            Open     = true                              // Keep annotation open on view.
        };

        // Apply the changes to all pages (1‑based indexing).
        editor.ModifyAnnotations(1, editor.Document.Pages.Count, updatedAnnotation);

        // Save the modified PDF.
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Annotation properties updated and saved to '{outputPath}'.");
    }
}
