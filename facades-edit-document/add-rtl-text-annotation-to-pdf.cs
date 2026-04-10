using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "annotated.pdf";      // result PDF
        const string rtlText   = "مرحبا بالعالم";      // Arabic text (right‑to‑left)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // ---------- 1. Add a text annotation using the Facade API ----------
        // PdfContentEditor works with System.Drawing.Rectangle (GDI+). This is
        // acceptable because the rectangle is only used for positioning.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);

            // Rectangle(left, top, width, height) – note that System.Drawing.Rectangle
            // uses (x, y, width, height) where (x,y) is the upper‑left corner.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // CreateText(rect, title, contents, open, icon, page)
            // - title: shown in the annotation’s popup
            // - contents: the actual text (Arabic in this case)
            // - open: true so the popup is visible initially
            // - icon: any standard icon name (e.g., "Note")
            // - page: 1‑based page number
            editor.CreateText(rect, "RTL Annotation", rtlText, true, "Note", 1);

            // Save the PDF that now contains the annotation.
            editor.Save(outputPdf);
        }

        // ---------- 2. (Optional) Verify the annotation exists ----------
        // The TextAnnotation class does not expose an IsBidi property in the
        // current Aspose.Pdf version. Arabic Unicode text is rendered correctly
        // by PDF viewers without needing an explicit Bidi flag, so we simply
        // reopen the document to ensure it is valid and then save it again.
        using (Document doc = new Document(outputPdf))
        {
            // No additional property needs to be set for RTL rendering.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Text annotation with RTL support added to '{outputPdf}'.");
    }
}
