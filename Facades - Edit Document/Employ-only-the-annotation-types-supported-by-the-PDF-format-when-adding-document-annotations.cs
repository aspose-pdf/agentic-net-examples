using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // for Aspose.Pdf specific types (e.g., AnnotationType)

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable – wrap in using.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF.
            editor.BindPdf(inputPath);

            // ------------------------------------------------------------
            // 1. Text annotation (sticky note) – AnnotationType.Text
            // ------------------------------------------------------------
            // Use System.Drawing.Rectangle to avoid ambiguity with Aspose.Pdf.Rectangle.
            var textRect = new System.Drawing.Rectangle(100, 600, 20, 20);
            editor.CreateText(
                textRect,
                "This is a note added via PdfContentEditor.",
                "AuthorName",
                true,               // open when the document is opened
                "Note Title",
                0);                 // default icon

            // ------------------------------------------------------------
            // 2. Highlight annotation – AnnotationType.Highlight
            // ------------------------------------------------------------
            var highlightRect = new System.Drawing.Rectangle(50, 500, 200, 20);
            // PdfContentEditor.CreateMarkup expects System.Drawing.Color.
            editor.CreateMarkup(
                highlightRect,
                "Highlighted text",
                (int)AnnotationType.Highlight,
                0,                  // no special flags
                System.Drawing.Color.Yellow);

            // ------------------------------------------------------------
            // 3. Rubber stamp annotation – AnnotationType.Stamp
            // ------------------------------------------------------------
            var stampRect = new System.Drawing.Rectangle(200, 400, 100, 50);
            // PdfContentEditor.CreateRubberStamp also expects System.Drawing.Color.
            editor.CreateRubberStamp(
                3,                              // page number (1‑based)
                stampRect,
                "Approved",                     // built‑in icon name
                "Approved stamp",               // annotation contents
                System.Drawing.Color.Green);        // stamp color

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations added and saved to '{outputPath}'.");
    }
}
