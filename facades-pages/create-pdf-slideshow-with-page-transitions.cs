using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "slideshow.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade with the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Set a transition effect (e.g., vertical blinds) for page changes
            editor.TransitionType = PdfPageEditor.BLINDV; // constant defined in PdfPageEditor
            editor.TransitionDuration = 2; // transition lasts 2 seconds

            // Set how long each page is displayed during the slideshow
            editor.DisplayDuration = 5; // each page shown for 5 seconds

            // Process all pages (null or empty array means all pages)
            editor.ProcessPages = null;

            // Apply the configured changes to the document
            editor.ApplyChanges();

            // Release resources held by the editor
            editor.Close();

            // Save the modified PDF (output format is PDF, so no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Slideshow PDF saved to '{outputPath}'.");
    }
}