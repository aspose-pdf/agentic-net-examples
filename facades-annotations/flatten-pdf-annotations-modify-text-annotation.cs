using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
// Alias the nested FlattenSettings class to avoid the ambiguous Form type
using FlattenSettings = Aspose.Pdf.Forms.Form.FlattenSettings;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the annotation editor
        using (var editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Page numbers are 1‑based in Aspose.Pdf. We need page 8.
            const int pageNumber = 8;
            // Zero‑based index of the annotation on that page (the third annotation).
            const int annotationIndex = 2;

            // Ensure the page and annotation exist
            if (editor.Document.Pages.Count < pageNumber)
            {
                Console.Error.WriteLine($"Page {pageNumber} does not exist in the document.");
                return;
            }

            var page = editor.Document.Pages[pageNumber];
            if (page.Annotations.Count <= annotationIndex)
            {
                Console.Error.WriteLine($"Annotation index {annotationIndex} does not exist on page {pageNumber}.");
                return;
            }

            // Retrieve the annotation and set its Open flag to false (only TextAnnotation has this property)
            var annotation = page.Annotations[annotationIndex];
            if (annotation is TextAnnotation textAnnot)
            {
                textAnnot.Open = false;
            }
            else
            {
                Console.Error.WriteLine("Target annotation is not a TextAnnotation; Open flag cannot be set.");
            }

            // Prepare flatten settings: disable CallEvents and enable ApplyRedactions
            var settings = new FlattenSettings
            {
                CallEvents = false,
                ApplyRedactions = true
            };

            // Flatten the annotations with the specified settings
            editor.FlatteningAnnotations(settings);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
