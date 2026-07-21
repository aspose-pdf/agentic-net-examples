using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_durations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Bind a PdfPageEditor to the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Set the display duration (in seconds) for each slide/page
            editor.DisplayDuration = 5; // each page will be shown for 5 seconds

            // Optional: set a transition effect and its duration
            // editor.TransitionType = PdfPageEditor.BLINDV; // example transition style
            // editor.TransitionDuration = 2; // transition lasts 2 seconds

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF (no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with slide durations to '{outputPath}'.");
    }
}