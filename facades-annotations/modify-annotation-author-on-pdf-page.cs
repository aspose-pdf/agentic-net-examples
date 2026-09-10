using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ------------------------------------------------------------
        // 1. Create a self‑contained sample PDF with three pages.
        //    Page 3 contains two text annotations whose author is
        //    "Old Author". This guarantees the file exists for the
        //    subsequent annotation‑editing step.
        // ------------------------------------------------------------
        using (var doc = new Document())
        {
            // Add three blank pages.
            doc.Pages.Add(); // page 1
            doc.Pages.Add(); // page 2
            var page3 = doc.Pages.Add(); // page 3

            // First annotation on page 3.
            var annot1 = new TextAnnotation(page3, new Aspose.Pdf.Rectangle(100, 600, 200, 650))
            {
                Title = "Old Author",   // author (Title) to be replaced
                Subject = "Sample",
                Contents = "First annotation"
            };
            page3.Annotations.Add(annot1);

            // Second annotation on page 3.
            var annot2 = new TextAnnotation(page3, new Aspose.Pdf.Rectangle(100, 500, 200, 550))
            {
                Title = "Old Author",
                Subject = "Sample",
                Contents = "Second annotation"
            };
            page3.Annotations.Add(annot2);

            // Save the seed PDF.
            doc.Save(inputPath);
        }

        // ------------------------------------------------------------
        // 2. Modify the author of annotations on page 3 using
        //    PdfAnnotationEditor.ModifyAnnotationsAuthor.
        // ------------------------------------------------------------
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Replace "Old Author" with "New Author" on page 3.
            editor.ModifyAnnotationsAuthor(
                start: 3,               // start page (1‑based)
                end:   3,               // end page   (1‑based)
                srcAuthor: "Old Author",
                desAuthor: "New Author"
            );

            editor.Save(outputPath);
        }

        Console.WriteLine($"Author modified and saved to '{outputPath}'.");
    }
}
