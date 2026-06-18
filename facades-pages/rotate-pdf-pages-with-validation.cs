using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "rotated_output.pdf";

        // Desired rotation in degrees (example value)
        int desiredRotation = 90; // Change as needed

        // Validate rotation – only 0, 90, 180, or 270 are allowed
        if (!IsValidRotation(desiredRotation))
        {
            Console.Error.WriteLine($"Invalid rotation value: {desiredRotation}. Allowed values are 0, 90, 180, 270.");
            return;
        }

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Create the PdfPageEditor facade and bind the source PDF
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(inputPdf);

            // Apply the validated rotation to all pages
            editor.Rotation = desiredRotation;

            // Save the modified PDF
            editor.Save(outputPdf);

            Console.WriteLine($"PDF saved with rotation {desiredRotation}° to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }

    // Helper method to check allowed rotation values
    static bool IsValidRotation(int angle)
    {
        return angle == 0 || angle == 90 || angle == 180 || angle == 270;
    }
}