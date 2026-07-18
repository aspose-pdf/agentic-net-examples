using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source 200‑page PDF
        const string outputPath = "output_fade.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block to ensure proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the facade that edits page properties
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Set the transition effect to "Fade" (DISSOLVE) and duration to 2 seconds
            editor.TransitionType = PdfPageEditor.DISSOLVE;   // Fade/Dissolve effect
            editor.TransitionDuration = 2;                    // Duration in seconds

            // Apply the changes to all pages (default ProcessPages = all)
            editor.ApplyChanges();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with Fade transition saved to '{outputPath}'.");
    }
}