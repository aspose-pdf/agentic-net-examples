using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Example usage:
        // args = new string[] { "output.pdf", "input1.pdf", "input2.pdf", "input3.pdf" };
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <output.pdf> <input1.pdf> [<input2.pdf> ...]");
            return;
        }

        string outputPath = args[0];
        string[] inputFiles = new string[args.Length - 1];
        Array.Copy(args, 1, inputFiles, 0, inputFiles.Length);

        // Verify that all input files exist
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
            // PdfFileEditor does not implement IDisposable, so no using block is needed
            PdfFileEditor editor = new PdfFileEditor();

            // Optional: close streams after operation (useful when working with streams)
            editor.CloseConcatenatedStreams = true;

            // Concatenate the files in the order they appear in the array
            bool success = editor.Concatenate(inputFiles, outputPath);

            if (success)
                Console.WriteLine($"Successfully concatenated {inputFiles.Length} files into '{outputPath}'.");
            else
                Console.Error.WriteLine("Concatenation failed. Check the editor's LastException for details.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during concatenation: {ex.Message}");
        }
    }
}