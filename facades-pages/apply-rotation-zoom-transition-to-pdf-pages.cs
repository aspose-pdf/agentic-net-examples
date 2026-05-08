using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PresentationPdfProcessor
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "presentation_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document srcDoc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(srcDoc);

                // Example: apply a 90° rotation to all pages
                editor.Rotation = 90;               // valid values: 0, 90, 180, 270

                // Example: set zoom to 150% (integer percentage required)
                editor.Zoom = 150;                 // 150 = 150%

                // Set a transition effect for slide changes (e.g., vertical blinds)
                editor.TransitionType = PdfPageEditor.BLINDV;

                // Duration of the transition effect in seconds (integer required)
                editor.TransitionDuration = 2;   // 2 seconds

                // Optional: set display duration for each page (how long the slide stays)
                editor.DisplayDuration = 5;         // 5 seconds per page

                // Apply all changes to the document
                editor.ApplyChanges();

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Presentation PDF saved to '{outputPath}'.");
    }
}
