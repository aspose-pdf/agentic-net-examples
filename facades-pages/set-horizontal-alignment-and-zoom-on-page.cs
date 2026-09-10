using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // HorizontalAlignment enum

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfPageEditor facade
        PdfPageEditor editor = new PdfPageEditor();

        // Bind the source PDF
        editor.BindPdf(inputPath);

        // Target only page 4 for editing
        editor.ProcessPages = new int[] { 4 };

        // Set horizontal alignment (center the content)
        editor.HorizontalAlignment = HorizontalAlignment.Center;

        // Set zoom level (150% for better readability)
        editor.Zoom = 1.5f;

        // Apply the changes to the document
        editor.ApplyChanges();

        // Save the modified PDF
        editor.Save(outputPath);

        // Clean up the facade
        editor.Close();

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
