using System;
using System.Collections.Generic;
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

        // Initialize the PdfPageEditor facade
        PdfPageEditor editor = new PdfPageEditor();

        // Bind the source PDF
        editor.BindPdf(inputPath);

        // Specify rotations for pages 3, 5 and 7 (1‑based indexing)
        // Using multiple entries in the PageRotations dictionary
        editor.PageRotations = new Dictionary<int, int>
        {
            { 3, 180 },
            { 5, 180 },
            { 7, 180 }
        };

        // Apply the changes to the document
        editor.ApplyChanges();

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}