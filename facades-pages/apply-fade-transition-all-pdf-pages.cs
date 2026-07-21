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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Set the transition type and duration.
            // Aspose.Pdf does not expose a dedicated "Fade" constant;
            // the DISSOLVE constant provides a similar fade effect.
            editor.TransitionType = PdfPageEditor.DISSOLVE; // fade-like transition
            editor.TransitionDuration = 2; // duration in seconds

            // By default ProcessPages applies to all pages, so no further configuration needed
            editor.ApplyChanges();

            // Save the modified document (PDF format)
            doc.Save(outputPath);

            // Close the facade (releases the bound document)
            editor.Close();
        }

        Console.WriteLine($"PDF with fade transition saved to '{outputPath}'.");
    }
}