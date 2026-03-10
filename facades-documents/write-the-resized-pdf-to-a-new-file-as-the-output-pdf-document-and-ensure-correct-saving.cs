using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "resized_output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfPageEditor facade to manipulate page properties
        PdfPageEditor editor = new PdfPageEditor();

        // Bind the source PDF document to the editor
        editor.BindPdf(inputPath);

        // Set the zoom factor (e.g., 0.5 = 50% of original size)
        editor.Zoom = 0.5f;

        // Save the modified PDF to the specified output file
        editor.Save(outputPath);

        // Close the editor to release any internal resources
        editor.Close();

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}