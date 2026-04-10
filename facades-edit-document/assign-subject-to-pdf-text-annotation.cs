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
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor (facade) to work with annotations.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPath);

            // Obtain a page instance – required for the TextAnnotation constructor.
            Page page = editor.Document.Pages[1];

            // Define the rectangle that determines the annotation's position and size.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);

            // Create a TextAnnotation using the (Page, Rectangle) constructor.
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Subject  = "ReviewNotes",               // subject for searching/categorization
                Title    = "Reviewer",                  // optional title
                Contents = "Please review this section.", // optional contents
                Color    = Aspose.Pdf.Color.Yellow      // optional visual color
            };

            // Apply the annotation changes to all pages (or specify a range).
            int startPage = 1;
            int endPage   = editor.Document.Pages.Count; // total pages in the bound document

            editor.ModifyAnnotations(startPage, endPage, annotation);

            // Save the updated PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation with subject saved to '{outputPath}'.");
    }
}
