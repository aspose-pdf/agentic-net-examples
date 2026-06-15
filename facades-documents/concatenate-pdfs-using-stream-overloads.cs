using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least an output file and two input PDFs
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: concatpdf <output.pdf> <input1.pdf> <input2.pdf> [more input PDFs...]");
            return;
        }

        string outputPath = args[0];
        string[] inputPaths = new string[args.Length - 1];
        Array.Copy(args, 1, inputPaths, 0, args.Length - 1);

        // Verify that all input files exist
        foreach (var path in inputPaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Prepare input streams array
        Stream[] inputStreams = new Stream[inputPaths.Length];
        try
        {
            for (int i = 0; i < inputPaths.Length; i++)
            {
                inputStreams[i] = new FileStream(inputPaths[i], FileMode.Open, FileAccess.Read);
            }

            // Open output stream (will be created/overwritten)
            using (Stream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                PdfFileEditor editor = new PdfFileEditor();
                // Close input streams automatically after concatenation (optional but safe)
                editor.CloseConcatenatedStreams = true;

                bool result = editor.Concatenate(inputStreams, outputStream);
                if (result)
                {
                    Console.WriteLine($"Successfully concatenated {inputPaths.Length} PDFs into '{outputPath}'.");
                }
                else
                {
                    Console.Error.WriteLine("Concatenation failed.");
                }
            }
        }
        finally
        {
            // Ensure all input streams are disposed even if an exception occurs
            foreach (var stream in inputStreams)
            {
                stream?.Dispose();
            }
        }
    }
}