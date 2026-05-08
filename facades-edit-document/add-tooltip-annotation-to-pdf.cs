using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_tooltip.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (a facade) to add a tooltip annotation.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF.
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (position and size).
            // PdfContentEditor.CreateText expects a System.Drawing.Rectangle.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 20, 20);

            // Title displayed in the popup window (optional).
            string title = "Info";

            // Contents displayed as a tooltip when the mouse hovers over the annotation.
            string contents = "Additional information displayed on hover.";

            // Open flag: false means the popup is not shown automatically.
            bool open = false;

            // Icon type for the annotation (e.g., "Note", "Comment", etc.).
            string icon = "Note";

            // Page number (1‑based indexing).
            int pageNumber = 1;

            // Create the text annotation which acts as a tooltip.
            editor.CreateText(rect, title, contents, open, icon, pageNumber);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Tooltip annotation added. Saved to '{outputPath}'.");
    }
}
