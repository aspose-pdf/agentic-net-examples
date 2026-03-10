using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files in the desired order
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Output file that will contain the concatenated result
        string outputFile = "merged.pdf";

        // Verify that all input files exist before attempting concatenation
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Use PdfFileEditor from Aspose.Pdf.Facades to concatenate the PDFs
        PdfFileEditor editor = new PdfFileEditor();
        bool concatenated = editor.Concatenate(inputFiles, outputFile);

        if (concatenated)
        {
            Console.WriteLine($"Successfully concatenated {inputFiles.Length} PDFs into '{outputFile}'.");
        }
        else
        {
            Console.Error.WriteLine("PDF concatenation failed.");
        }
    }
}