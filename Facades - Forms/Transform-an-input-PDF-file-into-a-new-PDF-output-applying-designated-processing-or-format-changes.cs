using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a facade that allows page‑level modifications
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document into the facade
            editor.BindPdf(inputPath);

            // Example transformation: rotate every page 90 degrees clockwise
            editor.Rotation = 90; // Valid values: 0, 90, 180, 270

            // Optional: change the output page size (uncomment if needed)
            // editor.PageSize = PageSize.A4;

            // Save the transformed PDF to the specified output file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transformed PDF saved to '{outputPath}'.");
    }
}