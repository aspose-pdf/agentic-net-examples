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

        // Edit page 7 zoom using PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);               // Load the PDF
            editor.ProcessPages = new int[] { 7 };   // Target only page 7
            editor.Zoom = 2.0f;                      // Set zoom to 200%
            editor.ApplyChanges();                   // Apply modifications
            editor.Save(outputPath);                 // Save the result
            editor.Close();                          // Release resources
        }

        Console.WriteLine($"Zoom set on page 7 and saved to '{outputPath}'.");
    }
}