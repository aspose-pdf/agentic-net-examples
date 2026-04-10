using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "slideshow_fade.pdf";

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

            // Set the transition type to Fade.
            // Aspose.Pdf uses integer values for transition types; 0 corresponds to Fade.
            editor.TransitionType = 0;

            // Set the transition duration to 2 seconds.
            editor.TransitionDuration = 2;

            // Apply the changes to all pages (ProcessPages defaults to all pages)
            editor.ApplyChanges();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Slideshow PDF saved to '{outputPath}'.");
    }
}