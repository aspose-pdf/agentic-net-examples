using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";
        const int targetPage    = 1; // page to rotate (1‑based index)

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

            // Preserve any existing transition settings (do not modify them)
            // Apply a 180° rotation to the desired page.
            // Use the PageRotations dictionary to affect a single page.
            editor.PageRotations[targetPage] = 180;

            // Save the modified PDF
            editor.Save(outputPath);
            editor.Close();

            Console.WriteLine($"Page {targetPage} rotated 180° and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}