using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Edit page properties using the Facades API
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Apply changes only to page 3 (1‑based indexing)
            editor.ProcessPages = new int[] { 3 };

            // Set zoom to 1.5 (150 % magnification)
            editor.Zoom = 1.5f;

            // Commit the modifications
            editor.ApplyChanges();

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 3 zoom set to 150 % and saved to '{outputPath}'.");
    }
}