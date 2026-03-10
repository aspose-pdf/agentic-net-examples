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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfPageEditor facade, bind the PDF, apply a change, and save.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the existing PDF document.
            editor.BindPdf(inputPath);

            // Example modification: set a zoom factor for the pages.
            editor.Zoom = 0.75f;

            // Persist the changes to a new PDF file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}