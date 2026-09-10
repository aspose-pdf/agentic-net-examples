using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Define input PDF files and the output file
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        string outputFile = "merged.pdf";

        // Verify that all input files exist before proceeding
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Log the files that will be processed
        Console.WriteLine("Starting PDF concatenation:");
        foreach (var file in inputFiles)
        {
            Console.WriteLine($"Input: {file}");
        }
        Console.WriteLine($"Output: {outputFile}");

        // Perform the concatenation using PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor
        {
            // Example setting: preserve logical structure during concatenation
            CopyLogicalStructure = true
        };

        bool success = editor.Concatenate(inputFiles, outputFile);

        // Report the result
        if (success)
        {
            Console.WriteLine("Concatenation completed successfully.");
        }
        else
        {
            Console.Error.WriteLine("Concatenation failed.");
            Console.Error.WriteLine(editor.ConversionLog);
        }
    }
}