using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to rotate pages
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Try setting an invalid rotation value
            try
            {
                editor.Rotation = 45; // Invalid: must be 0, 90, 180, or 270
                editor.ApplyChanges(); // Triggers validation
            }
            catch (InvalidValueFormatException ex)
            {
                Console.WriteLine($"Invalid rotation value: {ex.Message}");
                // Set a valid rotation instead
                editor.Rotation = 90;
                editor.ApplyChanges();
            }

            // Save the rotated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}