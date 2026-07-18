using System;
using System.IO;
using System.Drawing;               // required for Rectangle
using Aspose.Pdf.Facades;          // PdfContentEditor facade

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string comment    = "User comment goes here.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF, create a sticky‑note (text) annotation on page 1, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (x, y, width, height).
            Rectangle annotRect = new Rectangle(100, 500, 200, 100);

            // CreateText creates a TextAnnotation (sticky note).
            // Parameters: rectangle, title, contents, open flag, icon name, page number.
            editor.CreateText(annotRect, "User", comment, true, "Note", 1);

            editor.Save(outputPath);
        }

        Console.WriteLine($"Sticky note added and saved to '{outputPath}'.");
    }
}