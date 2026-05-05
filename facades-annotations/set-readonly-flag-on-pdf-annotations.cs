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
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_readonly.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the annotation editor facade
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdf);

            // Retrieve a page to use for constructing the annotation instance
            Page samplePage = editor.Document.Pages[1];

            // Create a dummy TextAnnotation (type determines which annotations are modified)
            // Rectangle values are irrelevant for the modify operation
            Aspose.Pdf.Rectangle dummyRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextAnnotation annotationToModify = new TextAnnotation(samplePage, dummyRect);

            // Set the read‑only flag
            annotationToModify.Flags = AnnotationFlags.ReadOnly;

            // Apply the modification to all pages (or specify a range)
            editor.ModifyAnnotations(1, editor.Document.Pages.Count, annotationToModify);

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with read‑only flag applied: {outputPdf}");
    }
}