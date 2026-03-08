using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that will be transformed into a booklet
        const string inputPath = "input.pdf";

        // Desired location for the generated booklet PDF
        const string outputPath = "booklet_output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor facade – no IDisposable, so no using block needed
        PdfFileEditor editor = new PdfFileEditor();

        // MakeBooklet writes the booklet directly to the specified output file
        bool created = editor.MakeBooklet(inputPath, outputPath);

        if (created)
        {
            Console.WriteLine($"Booklet successfully saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to create booklet.");
        }
    }
}