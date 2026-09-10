using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade with the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Set the transition type for page 1 to Fade.
            // Fade corresponds to the integer value 0 in the PDF specification.
            editor.TransitionType = 0; // Fade transition

            // Set the transition duration to 2 seconds.
            editor.TransitionDuration = 2;

            // Apply the changes made to the document pages.
            editor.ApplyChanges();

            // Save the modified PDF to the output file.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with slide transition to '{outputPath}'.");
    }
}