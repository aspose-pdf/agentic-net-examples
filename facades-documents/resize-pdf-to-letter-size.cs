using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_letter.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Letter size in points (1 inch = 72 points)
        double letterWidth = 8.5 * 72; // 612 points
        double letterHeight = 11 * 72; // 792 points

        PdfFileEditor editor = new PdfFileEditor();

        // Resize all pages to Letter size and save to outputPath
        bool success = editor.ResizeContents(inputPath, outputPath, null, letterWidth, letterHeight);

        if (success)
            Console.WriteLine($"PDF resized to Letter size and saved as '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to resize PDF.");
    }
}