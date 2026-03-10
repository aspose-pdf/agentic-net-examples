using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for page editing

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Rotate page 2 by 90 degrees (allowed values: 0, 90, 180, 270)
        // You can modify the page numbers and rotation angle as needed.
        int[] pagesToRotate = { 2 };          // 1‑based page numbers
        int   rotationAngle = 90;             // rotation in degrees

        // Use PdfPageEditor (implements IDisposable) to edit page properties
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file
            editor.BindPdf(inputPath);

            // Specify which pages will be processed
            editor.ProcessPages = pagesToRotate;

            // Set the desired rotation for the selected pages
            editor.Rotation = rotationAngle;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Persist the modified PDF to the output file
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
    }
}