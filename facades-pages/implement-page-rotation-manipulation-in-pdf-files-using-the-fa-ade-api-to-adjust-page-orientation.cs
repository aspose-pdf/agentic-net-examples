using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade and bind the PDF document
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Define rotation for specific pages (1‑based indexing)
        // Allowed values: 0, 90, 180, 270
        editor.PageRotations = new System.Collections.Generic.Dictionary<int, int>
        {
            { 1, 90 },   // Rotate first page 90°
            { 2, 180 }   // Rotate second page 180°
            // Other pages keep their original orientation (default 0)
        };

        // Apply the rotation changes
        editor.ApplyChanges();

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}