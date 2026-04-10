using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_aligned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Set horizontal alignment to left for all pages
            editor.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Left;

            // Apply the alignment changes
            editor.ApplyChanges();

            // Save the edited PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Horizontal alignment applied and saved to '{outputPath}'.");
    }
}