using System;
using System.Drawing;               // Required for Rectangle and Color (used by PdfContentEditor)
using Aspose.Pdf.Facades;          // PdfContentEditor resides here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // existing PDF
        const string outputPdf = "output_caret.pdf";   // result PDF (PS output not supported)

        // Verify input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create the editor, bind the PDF, add a caret annotation, and save.
        PdfContentEditor editor = new PdfContentEditor();

        // Load the existing PDF document
        editor.BindPdf(inputPdf);

        // Define the annotation rectangle (location on the page)
        Rectangle annotRect = new Rectangle(100, 500, 150, 550);   // x, y, width, height

        // Define the caret rectangle (the actual caret shape)
        Rectangle caretRect = new Rectangle(110, 510, 130, 530);

        // Symbol can be "P" (Paragraph) or "None"
        string symbol = "None";

        // Optional contents displayed in the annotation popup
        string contents = "Caret annotation added via PdfContentEditor";

        // Annotation color
        Color color = Color.Red;

        // Page numbers are 1‑based; here we add the annotation to the first page
        int pageNumber = 1;

        // Create the caret annotation
        editor.CreateCaret(pageNumber, annotRect, caretRect, symbol, contents, color);

        // Save the modified document. PS format is not supported for export; we save as PDF.
        editor.Save(outputPdf);

        // Clean up
        editor.Close();

        Console.WriteLine($"Caret annotation added and saved to '{outputPdf}'.");
    }
}