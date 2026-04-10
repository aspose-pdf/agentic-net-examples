using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and desired rotation (degrees)
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";
        int desiredRotation = 90; // Example value; replace as needed

        // Validate that the rotation is one of the allowed values
        int[] allowedRotations = { 0, 90, 180, 270 };
        if (Array.IndexOf(allowedRotations, desiredRotation) < 0)
        {
            Console.Error.WriteLine($"Invalid rotation value: {desiredRotation}. Allowed values are 0, 90, 180, 270.");
            return;
        }

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the source PDF
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(inputPath);

            // Apply the validated rotation to all pages
            editor.Rotation = desiredRotation;

            // Save the modified PDF
            editor.Save(outputPath);
            Console.WriteLine($"PDF saved with rotation {desiredRotation}° to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}