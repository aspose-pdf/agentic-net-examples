using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_page2.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create PdfPageEditor, bind the PDF, set rotation for page 2, apply changes and save
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // PageRotations is a Dictionary<int, int> where key = page number (1‑based)
            // Set page 2 rotation to 90 degrees
            editor.PageRotations[2] = 90;

            // Apply the rotation changes
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 2 rotated by 90° and saved to '{outputPath}'.");
    }
}