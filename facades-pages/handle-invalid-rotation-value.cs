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

        // Example of an invalid rotation value (must be 0, 90, 180, or 270)
        int rotation = 45;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // PdfPageEditor implements IDisposable, so wrap it in a using block
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Load the PDF file
                editor.BindPdf(inputPath);

                // Attempt to assign the rotation; catch invalid values
                try
                {
                    editor.Rotation = rotation; // Throws InvalidValueFormatException if invalid
                }
                catch (InvalidValueFormatException)
                {
                    Console.Error.WriteLine($"Invalid rotation value: {rotation}. Using default rotation (0).");
                    editor.Rotation = 0; // Fallback to a valid rotation
                }

                // Apply the rotation change and save the result
                editor.ApplyChanges();
                editor.Save(outputPath);
            }

            Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}