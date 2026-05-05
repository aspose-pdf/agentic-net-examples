using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_modified.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Wrap the PdfAnnotationEditor in a using block for deterministic disposal
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Get a reference to the first page (any page can be used for the constructor)
            Page firstPage = editor.Document.Pages[1];

            // Define a rectangle for the annotation (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create a new TextAnnotation using the (Page, Rectangle) constructor
            TextAnnotation customAnnot = new TextAnnotation(firstPage, rect)
            {
                Title    = "Custom Flag Annotation",
                Contents = "This annotation has custom flags applied.",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,
                // Combine any flags you need
                Flags    = AnnotationFlags.NoZoom | AnnotationFlags.Locked
            };

            // Modify annotations on pages 1 through the last page
            // (start and end are 1‑based page numbers)
            editor.ModifyAnnotations(1, editor.Document.Pages.Count, customAnnot);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}
