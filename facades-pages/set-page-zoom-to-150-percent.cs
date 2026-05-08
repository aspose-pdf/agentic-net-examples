using System;
using System.IO;
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

        // Adjust zoom of page 3 to 150% using PdfPageEditor (Facades API)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF file
            editor.BindPdf(inputPath);

            // Process only page 3 (1‑based indexing)
            editor.ProcessPages = new int[] { 3 };

            // Set zoom factor (1.0 = 100%)
            editor.Zoom = 1.5f;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom adjusted PDF saved to '{outputPath}'.");
    }
}