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
        const string outputPathLiteral = "output_literal.pdf";
        const string outputPathVariable = "output_variable.pdf";

        // Ensure the input PDF exists and contains the annotations we will delete.
        if (!File.Exists(inputPath))
        {
            CreateSamplePdfWithAnnotation(inputPath);
        }

        // Delete annotation using a string literal
        using (PdfAnnotationEditor editorLiteral = new PdfAnnotationEditor())
        {
            editorLiteral.BindPdf(inputPath);
            editorLiteral.DeleteAnnotation("4cfa69cd-9bff-49e0-9005-e22a77cebf38");
            editorLiteral.Save(outputPathLiteral);
        }

        // Delete annotation using a variable
        string annotationName = "a1b2c3d4-e5f6-7890-abcd-ef1234567890";
        using (PdfAnnotationEditor editorVariable = new PdfAnnotationEditor())
        {
            editorVariable.BindPdf(inputPath);
            editorVariable.DeleteAnnotation(annotationName);
            editorVariable.Save(outputPathVariable);
        }
    }

    private static void CreateSamplePdfWithAnnotation(string path)
    {
        // Create a simple one‑page PDF document.
        var doc = new Document();
        var page = doc.Pages.Add();

        // First annotation – will be removed using a string literal.
        var annotationLiteral = new TextAnnotation(page, new Aspose.Pdf.Rectangle(100, 600, 200, 650))
        {
            Name = "4cfa69cd-9bff-49e0-9005-e22a77cebf38",
            Title = "Literal",
            Contents = "Annotation to be deleted via literal."
        };
        page.Annotations.Add(annotationLiteral);

        // Second annotation – will be removed using a variable.
        var annotationVariable = new TextAnnotation(page, new Aspose.Pdf.Rectangle(100, 500, 200, 550))
        {
            Name = "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
            Title = "Variable",
            Contents = "Annotation to be deleted via variable."
        };
        page.Annotations.Add(annotationVariable);

        doc.Save(path);
    }
}