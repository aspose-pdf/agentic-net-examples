using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Output merged PDF file
        const string outputFile = "merged.pdf";

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Create the PdfFileEditor facade (does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Optional: preserve outlines and logical structure during concatenation
        editor.CopyOutlines = true;
        editor.CopyLogicalStructure = true;

        // Perform concatenation of the input PDFs into a single document
        bool merged = editor.Concatenate(inputFiles, outputFile);

        if (merged)
        {
            Console.WriteLine($"Successfully merged PDFs into '{outputFile}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to merge PDF files.");
        }
    }
}