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

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Set rotation (must be 0, 90, 180, or 270 degrees)
            editor.Rotation = 90;

            // Set zoom factor (1.0 = 100%)
            editor.Zoom = 1.5f;

            // Choose a transition effect for slide changes
            editor.TransitionType = PdfPageEditor.BLINDV; // vertical blinds

            // Set how long the transition lasts (seconds)
            editor.TransitionDuration = 2;

            // Set how long each page is displayed during the presentation (seconds)
            editor.DisplayDuration = 5;

            // Apply the changes to all pages
            editor.ApplyChanges();

            // Save the modified PDF as a presentation PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Presentation PDF saved to '{outputPath}'.");
    }
}