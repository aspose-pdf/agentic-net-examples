using System;
using System.IO;
using System.Drawing;               // needed for System.Drawing.Rectangle
using Aspose.Pdf.Facades;          // PdfContentEditor resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "tooltip_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (facade) to edit the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF document
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (position and size)
            // Here we place a small square at (100,500) with width=20, height=20
            Rectangle rect = new Rectangle(100, 500, 20, 20);

            // Title appears in the annotation window (if opened)
            // Contents is shown as a tooltip when the mouse hovers over the annotation
            string title    = "Info";
            string contents = "This is additional information displayed as a tooltip.";

            // open = false so the annotation is not displayed open by default
            // icon = "Note" (any of the supported icon names can be used)
            editor.CreateText(rect, title, contents, false, "Note", 1);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Tooltip annotation added. Saved to '{outputPath}'.");
    }
}