using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string title      = "Note";
        const string contents   = "Custom annotation content";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PDF content editor (facade)
        PdfContentEditor editor = new PdfContentEditor();
        try
        {
            // Load the existing PDF document
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (x, y, width, height)
            // Coordinates are in points; (100,200) is the lower‑left corner.
            Rectangle rect = new Rectangle(100, 200, 200, 100);

            // Create a text (sticky‑note) annotation on page 1
            // Parameters: rectangle, title, contents, open flag, icon name, page number
            editor.CreateText(rect, title, contents, true, "Note", 1);

            // Save the modified PDF
            editor.Save(outputPath);
        }
        finally
        {
            // Release resources held by the editor
            editor.Close();
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}