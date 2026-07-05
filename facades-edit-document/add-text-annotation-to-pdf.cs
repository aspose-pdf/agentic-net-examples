using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string title      = "Note Title";
        const string contents   = "This is a custom text annotation.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor does not implement IDisposable, so we manage its lifetime manually.
        PdfContentEditor editor = new PdfContentEditor();
        try
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPath);

            // Create a text (sticky‑note) annotation on page 1.
            // Rectangle(x, y, width, height) – coordinates are in points.
            // Here we place a small 20×20 rectangle at (100,200).
            editor.CreateText(
                new System.Drawing.Rectangle(100, 200, 20, 20), // annotation rectangle
                title,                                          // annotation title
                contents,                                       // annotation contents
                false,                                          // open flag (false = closed)
                "Note",                                         // icon name (e.g., Note, Comment, etc.)
                1                                               // page number (1‑based)
            );

            // Save the modified PDF.
            editor.Save(outputPath);
        }
        finally
        {
            // Ensure resources are released.
            editor.Close();
        }

        Console.WriteLine($"Text annotation added and saved to '{outputPath}'.");
    }
}