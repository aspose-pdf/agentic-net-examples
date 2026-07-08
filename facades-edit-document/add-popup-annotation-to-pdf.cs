using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "popup_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (Facade) to add a popup annotation
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF document
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (position and size) using System.Drawing.Rectangle
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Create a popup annotation with the desired note.
            // Parameters: rectangle, contents, open flag (false = not initially open), page number (1‑based)
            editor.CreatePopup(rect, "This is a helpful note that appears when you hover over the area.", false, 1);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Popup annotation added. Saved to '{outputPath}'.");
    }
}