using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: one or more input PDFs and one output PDF.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <input1.pdf> [<input2.pdf> ...] <output.pdf>");
            return;
        }

        // The last argument is the output file; all preceding arguments are input files.
        string outputPath = args[args.Length - 1];
        string[] inputFiles = new string[args.Length - 1];
        Array.Copy(args, inputFiles, args.Length - 1);

        // Verify that each input file exists.
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
            // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block.
            PdfFileEditor editor = new PdfFileEditor();

            // Concatenate the input PDFs into the specified output PDF.
            bool success = editor.Concatenate(inputFiles, outputPath);

            if (success)
            {
                Console.WriteLine($"Successfully concatenated {inputFiles.Length} files into '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Concatenation failed. Check the editor's LastException for details.");
                if (editor.LastException != null)
                {
                    Console.Error.WriteLine($"Error: {editor.LastException.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}