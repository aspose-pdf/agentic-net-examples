using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class UpdateAnnotationModifiedDate
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create the annotation editor facade
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // A temporary TextAnnotation is required only to carry the Modified date.
            // TextAnnotation does not have a parameter‑less constructor – it must be created
            // with a Page and a Rectangle (any rectangle, even zero‑size, works for this purpose).
            Page page = editor.Document.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextAnnotation tempAnnot = new TextAnnotation(page, rect)
            {
                Modified = DateTime.Now // set to current system time
            };

            // Apply the modification to all annotations on pages 1 through the last page.
            int startPage = 1;
            int endPage   = editor.Document.Pages.Count; // total pages in the bound document
            editor.ModifyAnnotations(startPage, endPage, tempAnnot);

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotation Modified dates updated and saved to '{outputPdf}'.");
    }
}
