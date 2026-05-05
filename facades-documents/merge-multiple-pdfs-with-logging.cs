using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Define the input PDF files to be merged and the output file name.
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        string outputFile = "merged.pdf";

        // Verify that all input files exist before attempting concatenation.
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Log the start of the merge operation and list all files involved.
        Console.WriteLine("Starting multi‑file merge using Aspose.Pdf.Facades.PdfFileEditor.");
        Console.WriteLine("Input files:");
        foreach (string file in inputFiles)
        {
            Console.WriteLine($"  {file}");
        }
        Console.WriteLine($"Output file: {outputFile}");

        // Create the PdfFileEditor instance and perform the concatenation.
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.Concatenate(inputFiles, outputFile);

        // Log the result of the operation.
        if (result)
        {
            Console.WriteLine($"Merge completed successfully. Output saved to '{outputFile}'.");
        }
        else
        {
            Console.Error.WriteLine("Merge failed.");
            Console.Error.WriteLine($"Conversion log: {editor.ConversionLog}");
        }
    }
}