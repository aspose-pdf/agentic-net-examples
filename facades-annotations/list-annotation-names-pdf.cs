using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify that the PDF file exists before trying to bind it.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the annotation editor and bind the PDF file
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Get total number of pages from the underlying Document
            int pageCount = editor.Document.Pages.Count;

            // Extract all annotations from the whole document (no type filter).
            // Explicitly cast null to AnnotationType[] to avoid overload ambiguity.
            IList<Annotation> annotations = editor.ExtractAnnotations(1, pageCount, (AnnotationType[])null);

            // List annotation names to the console for debugging
            foreach (Annotation annot in annotations)
            {
                Console.WriteLine($"Annotation Name: {annot.Name}");
            }
        }
    }
}
