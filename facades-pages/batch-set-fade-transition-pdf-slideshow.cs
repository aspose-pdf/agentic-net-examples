using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_fade.pdf";

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

            // Set the transition effect to Fade (using DISSOLVE as the closest built‑in constant)
            editor.TransitionType = PdfPageEditor.DISSOLVE; // Fade effect
            // Set the transition duration to 2 seconds
            editor.TransitionDuration = 2;

            // Apply the changes to all pages (default ProcessPages processes every page)
            editor.ApplyChanges();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with fade transition saved to '{outputPath}'.");
    }
}