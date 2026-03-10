using System;
using System.IO;
using System.Drawing;               // Required for Rectangle (used by PdfContentEditor)
using Aspose.Pdf.Facades;          // Facade API for annotation handling

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable, so use a using block for deterministic cleanup
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (x, y, width, height)
            // Coordinates are measured from the lower‑left corner of the page
            Rectangle rect = new Rectangle(100, 500, 200, 100);

            // Annotation properties
            string title    = "Note Title";                         // Title shown in the annotation popup
            string contents = "This is a customizable text annotation."; // Main text of the annotation
            bool   open     = true;                                 // Show the popup open by default
            string icon     = "Comment";                            // Icon type (Comment, Key, Note, Help, NewParagraph, Paragraph, Insert)
            int    page     = 1;                                    // 1‑based page index

            // Create the text (sticky‑note) annotation on the specified page
            editor.CreateText(rect, title, contents, open, icon, page);

            // Persist the changes to a new PDF file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}