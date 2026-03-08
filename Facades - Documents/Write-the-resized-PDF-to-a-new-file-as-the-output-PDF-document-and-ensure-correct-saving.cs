using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfPageEditor facade
        PdfPageEditor editor = new PdfPageEditor();

        // Load the source PDF
        editor.BindPdf(inputPath);

        // Set the desired zoom factor (e.g., 0.5 for 50% size)
        editor.Zoom = 0.5f;

        // Save the resized PDF to a new file
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}