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
        int rotationToApply = 45;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Initialize the PdfPageEditor facade and bind the loaded document
                PdfPageEditor editor = new PdfPageEditor();
                editor.BindPdf(doc);

                // Attempt to set the rotation; catch invalid values
                try
                {
                    editor.Rotation = rotationToApply; // May throw InvalidValueFormatException
                }
                catch (InvalidValueFormatException ex)
                {
                    // Handle the invalid rotation gracefully
                    Console.Error.WriteLine($"Invalid rotation value: {rotationToApply}. Allowed values are 0, 90, 180, 270.");
                    Console.Error.WriteLine($"Error: {ex.Message}");
                    // Fallback to a valid rotation (e.g., 0 degrees)
                    editor.Rotation = 0;
                }

                // Apply the changes made by the editor
                editor.ApplyChanges();

                // Save the modified document
                doc.Save(outputPath);

                // Close the editor (releases the bound document)
                editor.Close();
            }

            Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected exceptions
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}