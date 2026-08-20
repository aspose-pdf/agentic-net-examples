using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class DeleteAnnotationsExample
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create a sample PDF with two annotations that have known names.
        // ---------------------------------------------------------------------
        const string inputPdf = "sample.pdf";
        const string literalAnnotationName = "4cfa69cd-9bff-49e0-9005-e22a77cebf38";
        const string variableAnnotationName = "d2a5b6e1-1234-5678-90ab-cdef12345678";

        // Build the PDF in‑memory and save it so the later examples can open it.
        using (Document doc = new Document())
        {
            // Add a single blank page.
            Page page = doc.Pages.Add();

            // First annotation – will be removed using a string literal.
            TextAnnotation annLiteral = new TextAnnotation(page, new Rectangle(100, 600, 200, 650))
            {
                Name = literalAnnotationName,
                Title = "Literal",
                Subject = "Demo",
                Contents = "Annotation to be deleted via literal"
            };
            page.Annotations.Add(annLiteral);

            // Second annotation – will be removed using a variable.
            TextAnnotation annVariable = new TextAnnotation(page, new Rectangle(100, 500, 200, 550))
            {
                Name = variableAnnotationName,
                Title = "Variable",
                Subject = "Demo",
                Contents = "Annotation to be deleted via variable"
            };
            page.Annotations.Add(annVariable);

            // Save the seed PDF.
            doc.Save(inputPdf);
        }

        // ---------------------------------------------------------------------
        // 2. Delete annotation using a string literal.
        // ---------------------------------------------------------------------
        PdfAnnotationEditor editorLiteral = new PdfAnnotationEditor();
        editorLiteral.BindPdf(inputPdf);
        // The name is supplied directly as a string literal.
        editorLiteral.DeleteAnnotation(literalAnnotationName);
        editorLiteral.Save("output_literal.pdf");
        editorLiteral.Close();

        // ---------------------------------------------------------------------
        // 3. Delete annotation using a variable.
        // ---------------------------------------------------------------------
        string annotationName = variableAnnotationName; // variable holding the GUID
        PdfAnnotationEditor editorVariable = new PdfAnnotationEditor();
        editorVariable.BindPdf(inputPdf);
        editorVariable.DeleteAnnotation(annotationName);
        editorVariable.Save("output_variable.pdf");
        editorVariable.Close();
    }
}
