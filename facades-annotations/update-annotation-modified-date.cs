using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_modified.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF and bind it to the annotation editor facade
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdf);

            // TextAnnotation does not have a parameter‑less constructor.
            // Use the (Page, Rectangle) constructor. A zero‑size rectangle is sufficient
            // because we only need the object to carry the Modified value for ModifyAnnotations.
            var dummyRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            Page firstPage = editor.Document.Pages[1];
            TextAnnotation tempAnnot = new TextAnnotation(firstPage, dummyRect)
            {
                Modified = DateTime.Now
            };

            // Apply the modification to all pages (1‑based indexing)
            int startPage = 1;
            int endPage   = editor.Document.Pages.Count;
            editor.ModifyAnnotations(startPage, endPage, tempAnnot);

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotation Modified date updated and saved to '{outputPdf}'.");
    }
}
