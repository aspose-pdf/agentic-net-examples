using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfPageEditor facade, bind the source PDF,
        // apply a simple modification (e.g., change zoom level),
        // and save the modified document.
        PdfPageEditor editor = new PdfPageEditor();

        // Bind the existing PDF document to the facade
        editor.BindPdf(inputPath);

        // Example modification: set the zoom factor for all pages
        // (this does not alter the visual content but demonstrates a change)
        editor.Zoom = 1.0f; // 100% zoom

        // Persist the changes to the output PDF file
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}