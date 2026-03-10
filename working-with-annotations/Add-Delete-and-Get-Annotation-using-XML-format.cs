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
        const string xfdfFile   = "annotations.xfdf";

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------
            // 1. Add a TextAnnotation to the first page
            // -------------------------------------------------
            Page page = doc.Pages[1]; // 1‑based indexing

            // Fully qualified rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title    = "SampleNote",
                Contents = "This is a sample text annotation.",
                Name     = "MyTextAnnotation", // set a name for later retrieval
                Color    = Aspose.Pdf.Color.Yellow
            };
            page.Annotations.Add(txtAnn);

            // Save the PDF with the new annotation
            doc.Save(outputPdf);

            // -------------------------------------------------
            // 2. Export all annotations to XFDF (XML format)
            // -------------------------------------------------
            doc.ExportAnnotationsToXfdf(xfdfFile);

            // -------------------------------------------------
            // 3. Delete the annotation we just added
            // -------------------------------------------------
            // AnnotationCollection uses 1‑based indexing
            page.Annotations.Delete(1); // deletes the first annotation on the page

            // Save the PDF after deletion (optional)
            doc.Save("output_deleted.pdf");

            // -------------------------------------------------
            // 4. Import annotations back from the XFDF file
            // -------------------------------------------------
            doc.ImportAnnotationsFromXfdf(xfdfFile);

            // Save the PDF after re‑importing
            doc.Save("output_restored.pdf");

            // -------------------------------------------------
            // 5. Retrieve the annotation by its Name and display its contents
            // -------------------------------------------------
            Annotation found = page.Annotations.FindByName("MyTextAnnotation");
            if (found is TextAnnotation txtFound)
            {
                Console.WriteLine($"Found annotation: Title='{txtFound.Title}', Contents='{txtFound.Contents}'");
            }
            else if (found != null)
            {
                // Fallback for other annotation types that may not expose Title/Contents
                Console.WriteLine($"Found annotation of type {found.GetType().Name}, but it does not expose Title/Contents.");
            }
            else
            {
                Console.WriteLine("Annotation not found.");
            }
        }
    }
}
