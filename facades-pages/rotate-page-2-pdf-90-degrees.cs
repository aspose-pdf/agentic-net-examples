using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_page2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfPageEditor facade and bind the source PDF
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Page numbers are 1‑based; set rotation of page 2 to 90 degrees
            editor.PageRotations[2] = 90;

            // Apply the rotation changes to the document
            editor.ApplyChanges();

            // Save the modified PDF to the output file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 2 rotated and saved to '{outputPath}'.");
    }
}