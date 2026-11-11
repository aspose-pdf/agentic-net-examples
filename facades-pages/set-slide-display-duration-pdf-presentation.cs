using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // required per task specification

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

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Desired display time (seconds) for each slide
        int displaySeconds = 5;
        // Integer value that represents the Fade transition (check Aspose.Pdf docs for exact value; 11 is typical for Fade)
        int fadeTransitionType = 11;

        // Use PdfPageEditor to set transition type and duration for all pages
        using (PdfPageEditor editor = new PdfPageEditor(doc))
        {
            // Apply to every page – null or an empty array means all pages
            editor.ProcessPages = null;
            editor.TransitionType = fadeTransitionType;   // integer transition type
            editor.TransitionDuration = displaySeconds;   // duration in seconds
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Presentation PDF saved to '{outputPath}'.");
    }
}
