using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // for AnnotationFlags

class Program
{
    static void Main()
    {
        const string tempPath = "temp.pdf";
        const string outputPath = "locked_annotation.pdf";

        // -------------------------------------------------
        // 1. Create a new PDF document with a single page
        // -------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a blank page (page size A4)
            doc.Pages.Add();

            // -------------------------------------------------
            // 2. Create a text annotation on the first page
            // -------------------------------------------------
            // Fully qualified rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(doc.Pages[1], rect)
            {
                Title    = "Note",
                Contents = "This annotation is locked and cannot be edited by end users."
            };

            // -------------------------------------------------
            // 3. Lock the annotation – set the Locked flag
            // -------------------------------------------------
            txtAnn.Flags = AnnotationFlags.Locked;

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(txtAnn);

            // -------------------------------------------------
            // 4. Save the document to a temporary file
            // -------------------------------------------------
            doc.Save(tempPath);
        }

        // -------------------------------------------------
        // 5. Use PdfAnnotationEditor (Facades API) to bind
        //    the temporary PDF and save the final output.
        // -------------------------------------------------
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(tempPath);          // Load the PDF created above
            editor.Save(outputPath);           // Save the final PDF (annotation remains locked)
        }

        // Clean up the temporary file
        if (File.Exists(tempPath))
        {
            File.Delete(tempPath);
        }

        Console.WriteLine($"PDF with locked annotation saved to '{outputPath}'.");
    }
}