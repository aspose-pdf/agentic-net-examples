using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string xfdfFile   = "annotations.xfdf";   // used as intermediate for MHT‑like handling
        const string annotationName = "MyNote";

        // -----------------------------------------------------------------
        // Load the PDF document (lifecycle rule: use Document constructor)
        // -----------------------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------------------
            // 1. Add a TextAnnotation to the first page
            // -------------------------------------------------------------
            Page page = doc.Pages[1];

            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title    = "Sample Title",
                Contents = "This is a sample text annotation.",
                Name     = annotationName,          // set a name for later retrieval
                Color    = Aspose.Pdf.Color.Yellow // use Aspose.Pdf.Color (cross‑platform)
            };

            // Add the annotation to the page (AnnotationCollection.Add)
            page.Annotations.Add(txtAnn);

            // -------------------------------------------------------------
            // 2. Export all annotations to XFDF (used here as the MHT‑compatible format)
            // -------------------------------------------------------------
            doc.ExportAnnotationsToXfdf(xfdfFile);

            // -------------------------------------------------------------
            // 3. Retrieve the annotation we just added by its name
            // -------------------------------------------------------------
            Annotation found = page.Annotations.FindByName(annotationName);
            if (found != null && found is TextAnnotation txtFound)
            {
                Console.WriteLine($"Found annotation: Title='{txtFound.Title}', Contents='{txtFound.Contents}'");
            }
            else
            {
                Console.WriteLine("Annotation not found or not a TextAnnotation.");
            }

            // -------------------------------------------------------------
            // 4. Delete the annotation (by object reference)
            // -------------------------------------------------------------
            if (found != null)
            {
                page.Annotations.Delete(found);
            }

            // -------------------------------------------------------------
            // 5. Import annotations back from the XFDF file (simulating MHT import)
            // -------------------------------------------------------------
            doc.ImportAnnotationsFromXfdf(xfdfFile);

            // -------------------------------------------------------------
            // 6. Save the modified PDF
            // -------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine("Add, delete, and get annotation operations completed.");
    }
}