using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_letter.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfPageEditor facade and bind the source PDF (file‑path overload)
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Set the page size to US Letter (8.5in x 11in = 612pt x 792pt)
        editor.PageSize = new PageSize(612f, 792f); // width, height in points

        // Save the resized document to a new file
        editor.Save(outputPath);

        Console.WriteLine($"PDF resized to Letter size and saved as '{outputPath}'.");
    }
}