using System;
using System.IO;
using System.Drawing;               // Required for System.Drawing.Rectangle
using Aspose.Pdf.Facades;          // Facade API for annotation handling

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Custom author metadata for the annotation
        const string author   = "John Doe";
        const string contents = "This note was added by John Doe.";

        // Define the annotation rectangle (x, y, width, height)
        Rectangle rect = new Rectangle(100, 500, 200, 100);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Add a text annotation using the Facade API.
        // The 'title' parameter stores the author information.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            editor.CreateText(rect, author, contents, true, "Note", 1);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation with author '{author}' saved to '{outputPath}'.");
    }
}