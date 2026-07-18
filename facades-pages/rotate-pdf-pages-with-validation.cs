using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Example rotation value – replace with user input as needed
        int rotation = 90;

        // Validate that the rotation is one of the allowed values
        int[] allowed = { 0, 90, 180, 270 };
        if (!allowed.Contains(rotation))
        {
            Console.Error.WriteLine($"Invalid rotation: {rotation}. Allowed values are 0, 90, 180, 270.");
            return;
        }

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (Aspose.Pdf.Facades) to apply the rotation
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Set rotation for all pages (validated above)
            editor.Rotation = rotation;

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with rotation {rotation}° to '{outputPath}'.");
    }
}