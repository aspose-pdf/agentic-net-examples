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

        // Create the PdfPageEditor facade, bind the PDF, set zoom for page 3, and save.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Edit only page 3.
            editor.ProcessPages = new int[] { 3 };

            // Set zoom factor to 1.5 (150%).
            editor.Zoom = 1.5f;

            // Apply the changes.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom adjusted PDF saved to '{outputPath}'.");
    }
}