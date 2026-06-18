using System;
using System.IO;
using System.Drawing;               // System.Drawing.Rectangle and Color are defined here
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string annotationContent = "Custom annotation text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor does NOT implement IDisposable; use explicit Close()
        PdfContentEditor editor = new PdfContentEditor();
        try
        {
            // Load the existing PDF
            editor.BindPdf(inputPath);

            // Define the annotation rectangle using System.Drawing.Rectangle (x, y, width, height).
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 200, 150, 100);

            // Create a text (sticky‑note) annotation.
            // Parameters: rect, title, contents, open flag, icon name, page number (1‑based).
            editor.CreateText(rect, "Note", annotationContent, false, "Note", 1);

            // Save the modified document.
            editor.Save(outputPath);
        }
        finally
        {
            // Release resources held by the facade.
            editor.Close();
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}
