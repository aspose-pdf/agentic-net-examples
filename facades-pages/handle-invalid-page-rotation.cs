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
        int desiredRotation = 45; // Example of an invalid rotation value

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade and bind the source PDF
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(inputPath);

            // Attempt to assign the rotation; handle invalid values gracefully
            try
            {
                // Valid values are 0, 90, 180, or 270
                editor.Rotation = desiredRotation;
            }
            catch (InvalidValueFormatException ex)
            {
                // Log the error and fall back to a safe default rotation
                Console.Error.WriteLine($"Invalid rotation ({desiredRotation}). Using default 0. Details: {ex.Message}");
                editor.Rotation = 0;
            }

            // Apply the rotation change and save the result
            editor.ApplyChanges();
            editor.Save(outputPath);
            editor.Close();

            Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}