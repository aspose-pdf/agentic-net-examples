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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Set the transition type to a fade-like effect (DISSOLVE) and duration (seconds)
            editor.TransitionType = PdfPageEditor.DISSOLVE; // Fade/Dissolve transition
            editor.TransitionDuration = 2; // 2‑second transition for each page

            // By default ProcessPages applies to all pages, so no further configuration needed
            editor.ApplyChanges();

            // Save the modified PDF using the facade's Save method
            editor.Save(outputPath);

            // Close the facade (releases the bound document)
            editor.Close();
        }

        Console.WriteLine($"PDF with Fade transition saved to '{outputPath}'.");
    }
}