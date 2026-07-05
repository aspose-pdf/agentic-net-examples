using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

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

        // Initialize the annotation editor and bind the PDF file
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // The editor works on the whole document, but we need a dummy TextAnnotation
            // instance to specify the flag we want to apply. Use the first page and a zero‑size rectangle.
            Page firstPage = editor.Document.Pages[1];
            Aspose.Pdf.Rectangle dummyRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextAnnotation readOnlyAnnot = new TextAnnotation(firstPage, dummyRect)
            {
                Flags = AnnotationFlags.ReadOnly
            };

            // Apply the flag to all pages (adjust range as needed)
            int startPage = 1;
            int endPage = editor.Document.Pages.Count; // total pages in the document

            editor.ModifyAnnotations(startPage, endPage, readOnlyAnnot);

            // Save the modified PDF
            editor.Save(outputPath);
            // editor.Close() is called automatically by the using block
        }

        Console.WriteLine($"Read‑only flag applied and saved to '{outputPath}'.");
    }
}
