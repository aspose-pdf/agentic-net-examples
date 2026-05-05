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

        // Create a new PDF document with a single page
        using (Document doc = new Document())
        {
            // Add a blank page (page index starts at 1)
            Page page = doc.Pages.Add();

            // Define the rectangle for the annotation (coordinates are in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation (you can choose any annotation type)
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Author",
                Contents = "This annotation is locked and cannot be edited by end users."
            };

            // Lock the annotation – set the Locked flag (prevents moving, resizing, deleting)
            annotation.Flags = AnnotationFlags.Locked;

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Use PdfAnnotationEditor (Facades API) to bind the document and save it
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);               // Load the in‑memory document into the facade
                editor.Save(outputPath);           // Persist the PDF with the locked annotation
            }
        }

        Console.WriteLine($"PDF with locked annotation saved to '{outputPath}'.");
    }
}