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

        // Adjust zoom of page 3 to 150% using PdfPageEditor (facade API)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Specify that only page 3 should be edited
            editor.ProcessPages = new int[] { 3 };

            // Set zoom coefficient (1.0 = 100%)
            editor.Zoom = 1.5f; // 150% magnification

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom-adjusted PDF saved to '{outputPath}'.");
    }
}