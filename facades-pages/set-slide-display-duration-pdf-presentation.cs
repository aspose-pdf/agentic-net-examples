using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int    slideDurationSeconds = 5; // duration for each slide

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade with the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Set the display duration (in seconds) for the pages
            editor.DisplayDuration = slideDurationSeconds;

            // Optional: set transition effect and its duration
            // editor.TransitionType = PdfPageEditor.BLINDV; // example transition
            // editor.TransitionDuration = 2;               // transition lasts 2 seconds

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF (presentation) to the output file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Presentation PDF saved with {slideDurationSeconds}s per slide to '{outputPath}'.");
    }
}