using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged (order matters)
        string[] inputFiles = new string[]
        {
            "file1.pdf",
            "file2.pdf",
            "file3.pdf"
        };

        // Output merged PDF file
        const string outputFile = "merged_output.pdf";

        // Verify that all input files exist before proceeding
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        try
        {
            // Create the PdfFileEditor instance (no IDisposable, so no using block)
            PdfFileEditor editor = new PdfFileEditor();

            // Optional: copy logical structure and outlines from source PDFs
            editor.CopyLogicalStructure = true;
            editor.CopyOutlines = true;

            // Log the concatenation operation details
            Console.WriteLine("Starting PDF concatenation operation...");
            Console.WriteLine("Input files:");
            foreach (string file in inputFiles)
            {
                Console.WriteLine($"  - {Path.GetFileName(file)}");
            }
            Console.WriteLine($"Output file: {Path.GetFileName(outputFile)}");

            // Perform the concatenation
            bool success = editor.Concatenate(inputFiles, outputFile);

            // Check result and log accordingly
            if (success)
            {
                Console.WriteLine("Concatenation completed successfully.");
            }
            else
            {
                Console.Error.WriteLine("Concatenation failed. See editor.ConversionLog for details:");
                Console.Error.WriteLine(editor.ConversionLog);
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected exceptions
            Console.Error.WriteLine($"Error during concatenation: {ex.Message}");
        }
    }
}