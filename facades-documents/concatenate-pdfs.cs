using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two input PDFs and one output path
        if (args == null || args.Length < 3)
        {
            Console.WriteLine("Usage: concatenate-pdfs <output.pdf> <input1.pdf> <input2.pdf> [<input3.pdf> ...]");
            return;
        }

        // First argument is the output file, the rest are input files
        string outputPath = args[0];
        string[] inputFiles = new string[args.Length - 1];
        for (int i = 0; i < inputFiles.Length; i++)
        {
            inputFiles[i] = args[i + 1];
        }

        // Verify that each input file exists
        foreach (string filePath in inputFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"Input file not found: {filePath}");
                return;
            }
        }

        try
        {
            Aspose.Pdf.Facades.PdfFileEditor pdfEditor = new Aspose.Pdf.Facades.PdfFileEditor();
            bool success = pdfEditor.Concatenate(inputFiles, outputPath);
            if (success)
            {
                Console.WriteLine($"Successfully concatenated {inputFiles.Length} PDFs into '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("PDF concatenation failed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}