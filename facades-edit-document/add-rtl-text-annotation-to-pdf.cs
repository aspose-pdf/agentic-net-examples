using System;
using System.Drawing;                     // Required for Rectangle and Color used by PdfContentEditor
using Aspose.Pdf.Facades;                // Facade API for annotation creation

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "annotated.pdf";      // result PDF

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor (facade) to add a text annotation.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // Define the annotation rectangle (x, y, width, height) in points.
            // Here we place the annotation near the top‑left of page 1.
            Rectangle annotRect = new Rectangle(100, 500, 200, 100);

            // Arabic text (right‑to‑left) to verify Unicode rendering.
            string rtlText = "مرحبا بالعالم"; // "Hello World" in Arabic

            // Create a text annotation:
            //   rect      – annotation rectangle
            //   title     – annotation title (shown in the popup)
            //   contents  – the actual text (Arabic)
            //   open      – true to display the popup open by default
            //   icon      – built‑in icon name (e.g., "Note")
            //   page      – 1‑based page number where the annotation is placed
            editor.CreateText(
                annotRect,
                "RTL Test",          // title
                rtlText,             // contents (right‑to‑left language)
                true,                // open flag
                "Note",              // icon
                1);                  // page number (first page)

            // Save the modified PDF. The Save method is the required lifecycle operation.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Text annotation added. Output saved to '{outputPdf}'.");
    }
}