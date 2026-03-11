using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API for page editing

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "rotated_output.pdf"; // result PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfPageEditor facade and bind the source PDF
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);               // load PDF (lifecycle rule)

        // Set the desired rotation for pages.
        // Valid values: 0, 90, 180, 270. Here we rotate every page by 90°.
        editor.Rotation = 90;                    // page rotation manipulation

        // Apply the changes to the document.
        editor.ApplyChanges();

        // Save the modified PDF (save lifecycle rule)
        editor.Save(outputPath);

        // Release resources held by the facade.
        editor.Close();

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}