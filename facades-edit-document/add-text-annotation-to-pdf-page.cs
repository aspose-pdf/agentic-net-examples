using System;
using System.IO;
using System.Drawing;               // Required for Rectangle
using Aspose.Pdf.Facades;          // Facade API for editing PDFs

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string annotationContent = "Custom annotation text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable, so wrap in using for deterministic disposal
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF
            editor.BindPdf(inputPath);

            // Define the annotation rectangle.
            // Rectangle(x, y, width, height) where (x,y) is the lower‑left corner in PDF points.
            Rectangle rect = new Rectangle(100, 200, 100, 100);

            // Create a text (sticky‑note) annotation.
            // Parameters: rect, title, contents, open flag, icon name, page number (1‑based)
            editor.CreateText(rect, "Note", annotationContent, true, "Note", 1);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Text annotation added and saved to '{outputPath}'.");
    }
}