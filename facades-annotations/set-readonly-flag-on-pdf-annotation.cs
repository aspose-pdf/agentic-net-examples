using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF with PdfAnnotationEditor (facade API)
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdf);

            // Determine the page range to work on (entire document)
            int startPage = 1;
            int endPage = editor.Document.Pages.Count;

            // Extract the first text annotation on the first page as an example.
            // Adjust the AnnotationType array as needed to target a specific type.
            IList<Annotation> annotations = editor.ExtractAnnotations(
                startPage,
                startPage,
                new AnnotationType[] { AnnotationType.Text });

            if (annotations == null || annotations.Count == 0)
            {
                Console.WriteLine("No matching annotation found.");
                editor.Save(outputPdf);
                return;
            }

            // Take the first annotation and set the ReadOnly flag.
            Annotation target = annotations[0];
            target.Flags = AnnotationFlags.ReadOnly;

            // Apply the modified annotation back to the document.
            // ModifyAnnotations expects a single Annotation instance, not an array.
            editor.ModifyAnnotations(startPage, endPage, target);

            // Save the updated PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotation updated and saved to '{outputPdf}'.");
    }
}
