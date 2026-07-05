using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "prepared.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the PdfPageEditor facade and bind the PDF file for editing
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(inputPath);

            // Rotation expects an integer (0, 90, 180, 270)
            pageEditor.Rotation = 0;
            // Zoom expects an integer percentage (100 = 100%)
            pageEditor.Zoom = 100;

            // Apply any pending changes (required before saving)
            pageEditor.ApplyChanges();

            // Save the (still unchanged) document to demonstrate the workflow
            pageEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF loaded and prepared: {outputPath}");
    }
}
