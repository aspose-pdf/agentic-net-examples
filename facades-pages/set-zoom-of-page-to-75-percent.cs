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

        // Edit the PDF using PdfPageEditor from Aspose.Pdf.Facades
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Restrict editing to page 5 (1‑based indexing)
            editor.ProcessPages = new int[] { 5 };

            // Set zoom to 0.75 (75 %)
            editor.Zoom = 0.75f;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 5 zoom set to 75% and saved to '{outputPath}'.");
    }
}