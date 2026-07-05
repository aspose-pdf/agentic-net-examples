using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the PdfPageEditor facade
        PdfPageEditor editor = new PdfPageEditor();

        // Load the PDF document into the facade
        editor.BindPdf(inputPath);

        // Set rotation for all pages (allowed values: 0, 90, 180, 270)
        editor.Rotation = 90; // rotate pages 90 degrees clockwise

        // Apply the rotation changes
        editor.ApplyChanges();

        // Save the rotated PDF
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();
    }
}