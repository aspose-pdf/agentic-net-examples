using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        string[] inputFiles = new string[]
        {
            "file1.pdf",
            "file2.pdf",
            "file3.pdf"
        };

        // Output merged PDF file
        string outputFile = "merged_output.pdf";

        // Verify that all input files exist before proceeding
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Log each input file name
        Console.WriteLine("Starting concatenation of the following files:");
        foreach (string file in inputFiles)
        {
            Console.WriteLine($" - {Path.GetFileName(file)}");
        }

        // Perform concatenation using Aspose.Pdf.Facades.PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();

        // Optional: configure editor properties (e.g., copy outlines, logical structure)
        editor.CopyOutlines = true;
        editor.CopyLogicalStructure = true;

        bool success = editor.Concatenate(inputFiles, outputFile);

        // Log the result
        if (success)
        {
            Console.WriteLine($"Concatenation succeeded. Output file: {Path.GetFileName(outputFile)}");
        }
        else
        {
            Console.Error.WriteLine("Concatenation failed.");
            // If needed, retrieve detailed log
            Console.Error.WriteLine($"Conversion Log: {editor.ConversionLog}");
        }
    }
}