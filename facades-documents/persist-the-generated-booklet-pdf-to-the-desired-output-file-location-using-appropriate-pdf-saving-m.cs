using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting booklet PDF
        const string inputPath = "input.pdf";
        const string outputPath = "booklet_output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor facade (provided by Aspose.Pdf.Facades)
        PdfFileEditor editor = new PdfFileEditor();

        // Generate the booklet; MakeBooklet writes the result directly to the output file
        bool success = editor.MakeBooklet(inputPath, outputPath);

        // Report the outcome
        if (success)
            Console.WriteLine($"Booklet successfully saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to create booklet.");
    }
}