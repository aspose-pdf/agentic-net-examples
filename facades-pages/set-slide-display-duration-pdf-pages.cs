using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int durationSeconds = 5; // display duration for each slide (in seconds)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade and bind the document
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Set the display duration for pages (applies to all pages by default)
            editor.DisplayDuration = durationSeconds;

            // Optional: set transition effect and its duration
            // editor.TransitionType = PdfPageEditor.BLINDV; // example transition style
            // editor.TransitionDuration = 2; // transition effect duration in seconds

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with {durationSeconds}s display duration per page to '{outputPath}'.");
    }
}