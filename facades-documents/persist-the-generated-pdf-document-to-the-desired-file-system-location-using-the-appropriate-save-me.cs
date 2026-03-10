using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfPageEditor facade and bind the source PDF
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Example operation: rotate the first page 90 degrees
        editor.ProcessPages = new int[] { 1 };   // 1‑based page numbers
        editor.Rotation     = 90;                // Allowed values: 0, 90, 180, 270

        // Apply any pending changes (optional; Save will also apply)
        editor.ApplyChanges();

        // Persist the modified document to the desired location
        editor.Save(outputPath);

        Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
    }
}