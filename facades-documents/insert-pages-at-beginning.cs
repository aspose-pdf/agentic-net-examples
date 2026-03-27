using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string destinationPath = "destination.pdf";
        const string sourcePath = "source.pdf";
        const string outputPath = "merged.pdf";

        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Insert pages from source.pdf at the beginning (position 1) of destination.pdf.
        // This example inserts pages 1 through 2 of the source PDF.
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Insert(destinationPath, 1, sourcePath, 1, 2, outputPath);

        if (success)
        {
            Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to insert pages.");
        }
    }
}
