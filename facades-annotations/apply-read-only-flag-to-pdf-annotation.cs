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
        const string outputPdf = "output.pdf";
        const int pageNum = 1; // page containing the target annotation

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the document to obtain the target Page object.
        Document doc = new Document(inputPdf);
        Page page = doc.Pages[pageNum];

        // Create a TextAnnotation using the (Page, Rectangle) constructor.
        // The rectangle can be zero‑size because we only modify the Flags.
        TextAnnotation annotationToModify = new TextAnnotation(page, new Aspose.Pdf.Rectangle(0, 0, 0, 0))
        {
            Flags = AnnotationFlags.ReadOnly
        };

        // PdfAnnotationEditor works with the Facades API.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        try
        {
            // Load the source PDF.
            editor.BindPdf(inputPdf);

            // Apply the modification to the specified page range (single page here).
            editor.ModifyAnnotations(pageNum, pageNum, annotationToModify);

            // Save the result.
            editor.Save(outputPdf);
        }
        finally
        {
            // Release resources held by the editor.
            editor.Close();
        }

        Console.WriteLine($"Read‑only flag applied and saved to '{outputPdf}'.");
    }
}
