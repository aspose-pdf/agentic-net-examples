using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor facade to modify page properties
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Restrict editing to page 7 (1‑based indexing)
            editor.ProcessPages = new int[] { 7 };

            // Set zoom factor to 2.0 (200 % magnification)
            editor.Zoom = 2.0f;

            // Apply the changes to the selected page(s)
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);

            // Close the facade (optional, called automatically by using)
            editor.Close();
        }

        Console.WriteLine($"Zoom set on page 7 and saved to '{outputPath}'.");
    }
}