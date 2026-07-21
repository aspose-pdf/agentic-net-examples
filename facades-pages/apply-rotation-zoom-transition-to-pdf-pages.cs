using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "presentation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade with the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Set rotation (allowed values: 0, 90, 180, 270)
            editor.Rotation = 90;

            // Set zoom factor (1.0 = 100%). 1.5 = 150% zoom – use a float literal
            editor.Zoom = 1.5f;

            // Optional: configure presentation transition effects
            editor.TransitionType = PdfPageEditor.BLINDV; // vertical blinds transition
            editor.TransitionDuration = 2;                // transition lasts 2 seconds
            editor.DisplayDuration = 5;                   // each page displayed for 5 seconds

            // Apply the changes to the document pages
            editor.ApplyChanges();

            // Save the modified PDF (output format is PDF by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Presentation PDF saved to '{outputPath}'.");
    }
}
