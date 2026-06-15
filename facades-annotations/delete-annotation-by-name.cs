using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class DeleteAnnotationByName
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a sample PDF that contains a named text annotation.
        // ------------------------------------------------------------
        using (Document document = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = document.Pages.Add();

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);

            // Create the text annotation using the (Page, Rectangle) constructor
            TextAnnotation textAnnotation = new TextAnnotation(page, rect);
            textAnnotation.Contents = "Sample annotation";
            textAnnotation.Name = "MyAnnotation"; // the name we will use for deletion

            // Add the annotation to the page
            page.Annotations.Add(textAnnotation);

            // Save the PDF – this file will be used in the next step
            document.Save("input.pdf");
        }

        // ------------------------------------------------------------
        // 2. Delete the annotation using a string literal.
        // ------------------------------------------------------------
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf("input.pdf");
            // The name is supplied directly as a literal string
            editor.DeleteAnnotation("MyAnnotation");
            editor.Save("output_literal.pdf");
        }

        // ------------------------------------------------------------
        // 3. Re‑create the sample PDF for the variable‑based example.
        // ------------------------------------------------------------
        using (Document document = new Document())
        {
            Page page = document.Pages.Add();
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);
            TextAnnotation textAnnotation = new TextAnnotation(page, rect);
            textAnnotation.Contents = "Sample annotation";
            textAnnotation.Name = "MyAnnotation";
            page.Annotations.Add(textAnnotation);
            document.Save("input2.pdf");
        }

        // ------------------------------------------------------------
        // 4. Delete the annotation using a variable that holds the name.
        // ------------------------------------------------------------
        string annotationName = "MyAnnotation"; // variable containing the annotation name
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf("input2.pdf");
            editor.DeleteAnnotation(annotationName);
            editor.Save("output_variable.pdf");
        }
    }
}
