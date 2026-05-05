using System;
using System.Drawing;               // Required for System.Drawing.Rectangle
using Aspose.Pdf.Facades;          // Facade API for editing PDF content

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Custom annotation details
        const string title    = "Note";
        const string contents = "Custom annotation content";

        // Verify the source PDF exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfContentEditor facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Define the annotation rectangle.
        // System.Drawing.Rectangle(x, y, width, height) where (x,y) is the lower‑left corner.
        // Adjust width/height as needed for the visual size of the note.
        Rectangle rect = new Rectangle(100, 200, 150, 100);

        // Create a text (sticky‑note) annotation on page 1.
        // Parameters: rectangle, title, contents, open flag, icon name, page number.
        editor.CreateText(rect, title, contents, true, "Note", 1);

        // Persist the changes to a new file.
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Text annotation added and saved to '{outputPath}'.");
    }
}