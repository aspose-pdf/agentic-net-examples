using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Rectangle
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "tooltip_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the editor
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Define the annotation rectangle (x, y, width, height) using System.Drawing.Rectangle
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 20, 20);

        // Title appears as tooltip, contents as popup text
        string title = "Info";
        string contents = "Additional information displayed on hover.";
        bool open = false;               // do not open the popup by default
        string icon = "Note";            // icon type for the annotation
        int pageNumber = 1;              // first page (1‑based indexing)

        // Create a text annotation (tooltip) on the specified page
        editor.CreateText(rect, title, contents, open, icon, pageNumber);

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"Tooltip annotation added. Saved to '{outputPath}'.");
    }
}