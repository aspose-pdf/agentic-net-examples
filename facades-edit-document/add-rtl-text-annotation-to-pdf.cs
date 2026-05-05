using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

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

        // Bind the PDF, add a right‑to‑left (RTL) text annotation, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Example RTL text (Arabic): "مرحبا بالعالم" (Hello World)
            string rtlContent = "مرحبا بالعالم";
            string title = "RTL Annotation";

            // System.Drawing.Rectangle (x, y, width, height) – coordinates are in points.
            Rectangle rect = new Rectangle(100, 500, 200, 100);

            // Create a sticky‑note text annotation with the RTL content.
            editor.CreateText(rect, title, rtlContent, true, "Note", 1);

            // Persist changes.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Text annotation added. Output saved to '{outputPath}'.");
    }
}