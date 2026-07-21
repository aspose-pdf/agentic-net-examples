using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Sample right‑to‑left (Arabic) text to verify Unicode rendering.
        string rtlText = "مرحبا بالعالم"; // "Hello World" in Arabic

        // Define the annotation rectangle (System.Drawing.Rectangle is required by the facade API).
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 100);

        // Use the PdfContentEditor facade to add a text annotation.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF.
            editor.BindPdf(inputPath);

            // Create a text annotation with RTL content.
            // Parameters: rectangle, title, contents, open flag, icon name, page number (1‑based).
            editor.CreateText(rect, "RTL Annotation", rtlText, true, "Note", 1);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}