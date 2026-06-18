using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

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

        // Bind the PDF with PdfAnnotationEditor (Facades)
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Create a dummy TextAnnotation using the (Page, Rectangle) constructor.
            // The rectangle can be zero‑size because we only need the annotation as a template
            // for the ModifyAnnotations call.
            Page firstPage = editor.Document.Pages[1];
            Aspose.Pdf.Rectangle dummyRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextAnnotation subjectAnnotation = new TextAnnotation(firstPage, dummyRect)
            {
                Subject = "ReviewNote"
            };

            // Apply the Subject to all TextAnnotations on all pages (1‑based indexing)
            editor.ModifyAnnotations(1, editor.Document.Pages.Count, subjectAnnotation);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Subject assigned and saved to '{outputPath}'.");
    }
}
