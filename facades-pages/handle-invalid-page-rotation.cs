using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor facade to edit page rotation
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document into the facade
            editor.BindPdf(inputPath);

            // Example of an invalid rotation value (must be 0, 90, 180, or 270)
            int invalidRotation = 45;

            try
            {
                // Attempt to assign the invalid rotation; this may throw InvalidValueFormatException
                editor.Rotation = invalidRotation;

                // Apply the changes and save the result
                editor.ApplyChanges();
                editor.Save(outputPath);
                Console.WriteLine($"PDF saved with rotation {invalidRotation}° to '{outputPath}'.");
            }
            catch (InvalidValueFormatException ex)
            {
                // Handle the specific exception for invalid rotation values
                Console.Error.WriteLine($"Invalid rotation value ({invalidRotation}°): {ex.Message}");

                // Fallback to a valid rotation (e.g., 0°) and save the document
                editor.Rotation = 0;
                editor.ApplyChanges();
                editor.Save(outputPath);
                Console.WriteLine($"PDF saved with default rotation (0°) to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
            }
            finally
            {
                // Ensure resources are released
                editor.Close();
            }
        }
    }
}