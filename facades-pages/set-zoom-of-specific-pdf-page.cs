using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor edits pages (rotate, zoom, size, etc.) via the Facades API
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Restrict editing to page 7 (Aspose.Pdf uses 1‑based page indexing)
            editor.ProcessPages = new int[] { 7 };

            // Set the zoom coefficient to 2.0 (200 % magnification)
            editor.Zoom = 2.0f;

            // Apply the modifications to the underlying document
            editor.ApplyChanges();

            // Save the edited PDF to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 7 zoom set to 2.0 and saved to '{outputPath}'.");
    }
}