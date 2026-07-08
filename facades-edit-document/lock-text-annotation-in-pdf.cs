using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "locked_annotation.pdf";

        // Create a new PDF document and add a single page.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (left, bottom, width, height).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text annotation.
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Author",
                Contents = "This annotation is locked.",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true
            };

            // Lock the annotation to prevent further modifications by end users.
            annotation.Flags = AnnotationFlags.Locked;

            // Add the annotation to the page.
            page.Annotations.Add(annotation);

            // Use the PdfAnnotationEditor facade to bind the document and save it.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with a locked annotation: {outputPath}");
    }
}