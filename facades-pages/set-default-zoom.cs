using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Set zoom to 1.0 (100%) for all pages
        editor.Zoom = 1.0f;

        // Apply changes and save the result
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Zoom set to 1.0 for all pages. Saved to '{outputPath}'.");
    }
}
