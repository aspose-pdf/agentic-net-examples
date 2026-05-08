using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // added for annotation types

class DeleteAnnotationExample
{
    static void Main()
    {
        // Input PDF containing annotations. If the file does not exist, create a simple PDF for demo purposes.
        string inputPath = "example.pdf";
        EnsureSamplePdfExists(inputPath);

        // ---------- Delete annotation using a string literal ----------
        string outputLiteral = "example_literal_out.pdf";
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF into the facade
            editor.BindPdf(inputPath);

            // Delete the annotation whose name is known as a literal string
            editor.DeleteAnnotation("4cfa69cd-9bff-49e0-9005-e22a77cebf38");

            // Save the modified PDF
            editor.Save(outputLiteral);
        }

        // ---------- Delete annotation using a variable ----------
        string outputVariable = "example_variable_out.pdf";
        // Annotation name stored in a variable (could be obtained at runtime)
        string annotName = "4cfa69cd-9bff-49e0-9005-e22a77cebf38";

        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the same PDF (or another one) into the facade
            editor.BindPdf(inputPath);

            // Delete the annotation using the variable
            editor.DeleteAnnotation(annotName);

            // Save the result
            editor.Save(outputVariable);
        }
    }

    /// <summary>
    /// Creates a minimal PDF file if the specified path does not exist.
    /// This allows the example to run without requiring an external file.
    /// </summary>
    private static void EnsureSamplePdfExists(string path)
    {
        if (File.Exists(path))
            return;

        // Create a simple one‑page PDF
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Add a sample annotation with the expected name so the DeleteAnnotation call succeeds.
            // Use the constructor that accepts a Page and a Rectangle (the overload that matches the types).
            var annotationRect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);
            var annotation = new TextAnnotation(page, annotationRect)
            {
                Name = "4cfa69cd-9bff-49e0-9005-e22a77cebf38",
                Contents = "Sample annotation"
                // Rect is already set by the constructor; no need to set it again.
            };
            page.Annotations.Add(annotation);

            doc.Save(path);
        }
    }
}
