using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "annotated_output.pdf";
        const string author    = "John Doe";               // custom author metadata
        const string noteText = "Review this section later.";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor (facade) to add a text annotation with the author as the Title.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // Define the annotation rectangle (x, y, width, height) in points.
            Rectangle rect = new Rectangle(100, 500, 200, 100);

            // Create the text annotation:
            //   rect   – location on the page
            //   author – Title property (author of the note)
            //   noteText – Contents of the annotation
            //   true   – annotation initially open
            //   "Note" – icon type (standard sticky‑note icon)
            //   1      – page number (1‑based indexing)
            editor.CreateText(rect, author, noteText, true, "Note", 1);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotation added with author \"{author}\" and saved to '{outputPdf}'.");
    }
}