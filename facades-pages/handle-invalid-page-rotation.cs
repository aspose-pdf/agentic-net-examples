using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (facade) to edit page rotation
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPath);

            try
            {
                // Attempt to assign an invalid rotation value (must be 0, 90, 180, or 270)
                editor.Rotation = 45; // Invalid – will trigger an exception
            }
            catch (InvalidValueFormatException ex)
            {
                // Handle the specific exception thrown for invalid rotation values
                Console.WriteLine($"Invalid rotation value: {ex.Message}");
                // Fallback to a valid rotation
                editor.Rotation = 90;
            }
            catch (Exception ex)
            {
                // Catch any other unexpected errors
                Console.WriteLine($"Unexpected error: {ex.Message}");
                // Optionally rethrow or exit
                return;
            }

            // Apply the rotation change to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}