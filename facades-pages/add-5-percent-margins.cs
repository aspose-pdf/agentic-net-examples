using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfFileEditor fileEditor = new PdfFileEditor();
        // null pages array processes all pages; add 5% margin on each side
        bool success = fileEditor.AddMarginsPct(inputPath, outputPath, null, 5, 5, 5, 5);
        if (success)
        {
            Console.WriteLine($"Margins added successfully. Saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to add margins.");
        }
    }
}