using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "edited_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the PdfPageEditor facade and bind the PDF file for editing
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(inputPath);

            // Example edit: rotate the first page 90 degrees
            pageEditor.Rotation = 90;               // rotation must be 0, 90, 180 or 270
            pageEditor.ProcessPages = new int[] {1}; // apply only to page 1 (optional)

            // Apply the pending changes
            pageEditor.ApplyChanges();

            // Save the edited document
            pageEditor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}