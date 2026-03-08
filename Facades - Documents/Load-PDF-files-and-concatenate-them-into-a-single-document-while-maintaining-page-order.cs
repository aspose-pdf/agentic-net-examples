using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API for file‑level operations

class Program
{
    static void Main()
    {
        // Input PDF files – order in the array defines the resulting page order
        string[] inputFiles = new string[]
        {
            "chapter1.pdf",
            "chapter2.pdf",
            "appendix.pdf"
        };

        // Output file that will contain all pages in the specified order
        const string outputFile = "merged_document.pdf";

        // Verify that all source files exist before attempting concatenation
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Source file not found: {file}");
                return;
            }
        }

        try
        {
            // PdfFileEditor does not implement IDisposable, so no using block is required
            PdfFileEditor editor = new PdfFileEditor();

            // Concatenate the PDFs; the method loads each file internally and writes the result
            bool success = editor.Concatenate(inputFiles, outputFile);

            if (success)
                Console.WriteLine($"Successfully concatenated {inputFiles.Length} files into '{outputFile}'.");
            else
                Console.Error.WriteLine("Concatenation failed.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors (e.g., I/O issues, corrupted PDFs)
            Console.Error.WriteLine($"Error during concatenation: {ex.Message}");
        }
    }
}