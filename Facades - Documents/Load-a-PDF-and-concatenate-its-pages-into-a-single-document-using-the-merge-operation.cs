using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Output file that will contain all pages
        string outputFile = "merged.pdf";

        // Verify that all input files exist before proceeding
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // PdfFileEditor provides the Concatenate method for merging PDFs
        PdfFileEditor editor = new PdfFileEditor();

        // Optional: preserve outlines and logical structure when concatenating
        editor.CopyOutlines = true;
        editor.CopyLogicalStructure = true;

        // Perform the concatenation; returns true on success
        bool success = editor.Concatenate(inputFiles, outputFile);

        if (success)
        {
            Console.WriteLine($"Successfully concatenated PDFs into '{outputFile}'.");
        }
        else
        {
            Console.Error.WriteLine("PDF concatenation failed.");
        }
    }
}