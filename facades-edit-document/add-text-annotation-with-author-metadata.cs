using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated.pdf";
        const string author     = "John Doe";                     // custom author metadata
        const string contents   = "This is a note with custom author metadata.";
        const int    pageNumber = 1;                               // page where annotation will be placed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade, bind the existing PDF, and add a text annotation.
        // The 'title' parameter of CreateText stores the author information.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Define the annotation rectangle (x, y, width, height) in points.
        Rectangle rect = new Rectangle(100, 500, 200, 100);

        // Create the annotation: title = author, contents = note text, open = true, icon = "Note".
        editor.CreateText(rect, author, contents, true, "Note", pageNumber);

        // Persist the changes.
        editor.Save(outputPath);

        Console.WriteLine($"Annotation with author '{author}' added and saved to '{outputPath}'.");
    }
}