using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_page2.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Rotate page 2 by 90 degrees using the PageRotations dictionary
        // PdfPageEditor is not IDisposable, so we do not wrap it in a using block.
        // The output stream is wrapped in a using block to ensure proper disposal.
        using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            PdfPageEditor editor = new PdfPageEditor();

            // Bind the source PDF (can be a file path or a stream)
            editor.BindPdf(inputPath);

            // Dictionary key = page number (1‑based), value = rotation angle (0, 90, 180, 270)
            editor.PageRotations = new Dictionary<int, int>
            {
                { 2, 90 }   // Rotate the second page by 90 degrees clockwise
            };

            // Save the modified PDF to the output stream
            editor.Save(outStream);
        }

        Console.WriteLine($"Page 2 rotated by 90° and saved to '{outputPath}'.");
    }
}