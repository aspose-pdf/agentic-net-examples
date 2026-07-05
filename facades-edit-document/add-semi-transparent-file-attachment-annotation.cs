using System;
using System.Drawing;               // For Rectangle
using Aspose.Pdf.Facades;          // Facade API for editing PDF content

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // Existing PDF
        const string outputPath = "output.pdf";     // PDF with the new annotation

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use the Facade to edit the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (position and size) on page 3
            // Rectangle(left, top, width, height) – coordinates are in points.
            Rectangle annotRect = new Rectangle(100, 500, 200, 100);

            // Create a file‑attachment annotation with 60 % opacity.
            // Parameters: rectangle, contents, file path, page number, icon name, opacity.
            editor.CreateFileAttachment(
                annotRect,
                "Sample attachment",          // Tooltip / contents
                "sample.txt",                 // File to attach (must exist)
                3,                            // Target page (page numbers are 1‑based)
                "Graph",                      // Icon name (Graph, PushPin, Paperclip, Tag)
                0.6);                         // Opacity (0 = fully transparent, 1 = opaque)

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation added with 0.6 opacity on page 3 → '{outputPath}'.");
    }
}