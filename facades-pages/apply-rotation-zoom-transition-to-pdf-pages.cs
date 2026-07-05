using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "presentation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade with the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Set a uniform rotation for all pages (allowed values: 0, 90, 180, 270)
            editor.Rotation = 90; // rotate 90 degrees clockwise

            // Set a uniform zoom factor – integer percentage (e.g., 150 = 150%)
            editor.Zoom = 150; // 150% zoom, integer required

            // Optional: define a transition effect for presentation mode
            editor.TransitionType = PdfPageEditor.BLINDV; // vertical blinds transition
            editor.TransitionDuration = 2; // duration in seconds, integer required

            // Apply the configured changes to the document pages
            editor.ApplyChanges();

            // Save the modified PDF back to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"Presentation PDF saved to '{outputPath}'.");
    }
}
