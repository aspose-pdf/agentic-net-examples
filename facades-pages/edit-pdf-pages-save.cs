using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "edited.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Apply page transformations
            editor.Rotation = 90;          // rotate pages 90 degrees
            editor.Zoom = 0.5f;            // zoom to 50%

            // Commit the changes
            editor.ApplyChanges();

            // Save the edited PDF to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}