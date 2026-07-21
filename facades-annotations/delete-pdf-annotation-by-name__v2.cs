using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class DeleteAnnotationExample
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputLiteralPath = "output_literal.pdf";
        const string outputVariablePath = "output_variable.pdf";

        // ------------------------------------------------------------
        // 1. Create a sample PDF that contains a named annotation.
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // The GUID that will be used as the annotation name.
            string annotationName = "4cfa69cd-9bff-49e0-9005-e22a77cebf38";

            // Create a simple text annotation and assign the name.
            TextAnnotation txtAnn = new TextAnnotation(page, new Aspose.Pdf.Rectangle(100, 600, 200, 650))
            {
                Name = annotationName,
                Title = "Sample",
                Contents = "This is a sample annotation"
            };
            page.Annotations.Add(txtAnn);

            // Save the PDF so that the later editor can open it.
            doc.Save(inputPath);
        }

        // ------------------------------------------------------------
        // 2. Delete the annotation using a string literal.
        // ------------------------------------------------------------
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath); // Load the PDF created above.
            editor.DeleteAnnotation("4cfa69cd-9bff-49e0-9005-e22a77cebf38"); // Delete by name (literal).
            editor.Save(outputLiteralPath); // Save the result.
        }

        // ------------------------------------------------------------
        // 3. Delete the annotation using a variable that holds the name.
        // ------------------------------------------------------------
        string annotationNameVar = "4cfa69cd-9bff-49e0-9005-e22a77cebf38";

        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            editor.DeleteAnnotation(annotationNameVar); // Delete by name (variable).
            editor.Save(outputVariablePath);
        }

        Console.WriteLine("Annotation deletion examples completed.");
    }
}
