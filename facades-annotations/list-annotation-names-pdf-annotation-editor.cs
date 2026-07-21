using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // ------------------------------------------------------------
        // Create a self‑contained input PDF so the sandbox has a file to open.
        // Add a page and a sample annotation with a Name set.
        // ------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add a blank page.
            Page page = seed.Pages.Add();

            // Define a rectangle for the annotation (left, bottom, right, top).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 600);

            // Create a TextAnnotation using the rectangle overload (older API versions may not expose the string overload).
            TextAnnotation sampleAnn = new TextAnnotation(page, rect);
            sampleAnn.Contents = "Sample annotation contents"; // set the annotation text
            sampleAnn.Name = "SampleAnnotation";               // optional name for debugging

            // Add the annotation to the page.
            page.Annotations.Add(sampleAnn);

            // Save the PDF to the expected path.
            seed.Save(inputPath);
        }

        // ------------------------------------------------------------
        // Now load the PDF with PdfAnnotationEditor and list annotation names.
        // ------------------------------------------------------------
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Access the underlying Document via the editor.
            Document doc = editor.Document;

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                AnnotationCollection annotations = doc.Pages[pageIndex].Annotations;

                // Iterate through all annotations on the page (1‑based indexing).
                for (int annIndex = 1; annIndex <= annotations.Count; annIndex++)
                {
                    Annotation annotation = annotations[annIndex];
                    Console.WriteLine($"Page {pageIndex}, Annotation Name: {annotation.Name}");
                }
            }
        }
    }
}
