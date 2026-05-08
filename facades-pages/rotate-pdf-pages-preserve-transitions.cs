using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Apply a 180-degree rotation.
        // This does not alter existing transition settings (TransitionType, TransitionDuration)
        // because those properties are preserved unless explicitly changed.
        editor.Rotation = 180; // rotates all pages; use PageRotations for per‑page control

        // Commit the modifications to the document
        editor.ApplyChanges();

        // Save the updated PDF
        editor.Save(outputPath);

        // Release resources associated with the bound document
        editor.Close();

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}