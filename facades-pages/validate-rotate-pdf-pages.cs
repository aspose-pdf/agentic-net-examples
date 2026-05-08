using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Valid rotation angles for PDF pages
    private static readonly int[] ValidRotations = { 0, 90, 180, 270 };

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "rotated_output.pdf";
        const int rotationToApply = 90; // Example rotation; replace with desired value

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Validate rotation value
        if (Array.IndexOf(ValidRotations, rotationToApply) < 0)
        {
            Console.Error.WriteLine($"Invalid rotation value: {rotationToApply}. Allowed values are 0, 90, 180, 270.");
            return;
        }

        try
        {
            // Bind the source PDF
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(inputPdf);

            // Apply the validated rotation to all pages
            editor.Rotation = rotationToApply;

            // Save the modified PDF
            editor.Save(outputPdf);
            editor.Close();

            Console.WriteLine($"PDF saved with rotation {rotationToApply}° to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during rotation: {ex.Message}");
        }
    }
}