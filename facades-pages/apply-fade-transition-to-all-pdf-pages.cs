using System;
using System.IO;
using Aspose.Pdf;                     // Document class
using Aspose.Pdf.Facades;            // PdfPageEditor facade

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the document to the editor
                editor.BindPdf(doc);

                // Set the transition type to Fade (DISSOLVE) for all pages
                editor.TransitionType = PdfPageEditor.DISSOLVE;

                // Set a uniform transition duration (e.g., 2 seconds)
                editor.TransitionDuration = 2;

                // Apply the changes to the document pages
                editor.ApplyChanges();
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Fade transition applied to all pages. Saved as '{outputPath}'.");
    }
}