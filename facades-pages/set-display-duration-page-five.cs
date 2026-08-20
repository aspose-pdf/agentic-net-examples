using System;
using System.IO;
using Aspose.Pdf;
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

        // Use PdfPageEditor to modify page display duration
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document into the facade
            editor.BindPdf(inputPath);

            // Edit only page 5 (1‑based indexing)
            editor.ProcessPages = new int[] { 5 };

            // Set the display duration to 5 seconds
            editor.DisplayDuration = 5;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 5 display duration set to 5 seconds. Saved to '{outputPath}'.");
    }
}