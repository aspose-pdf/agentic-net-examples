using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_A3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (Facade) to modify page size
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Set the output page size to A3 (420 mm × 297 mm)
            editor.PageSize = PageSize.A3;

            // Optional: increase resolution by scaling the content
            // editor.Zoom = 2.0; // uncomment to double the resolution

            // Save the result
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"PDF saved with A3 page size to '{outputPath}'.");
    }
}