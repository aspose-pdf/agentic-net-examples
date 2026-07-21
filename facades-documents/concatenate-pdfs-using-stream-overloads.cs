using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two input PDFs and one output path:
        //   args[0..n-2] = input files, args[n-1] = output file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: concat <input1.pdf> <input2.pdf> [<input3.pdf> ...] <output.pdf>");
            return;
        }

        // Separate input and output arguments
        string outputPath = args[args.Length - 1];
        string[] inputPaths = new string[args.Length - 1];
        Array.Copy(args, inputPaths, args.Length - 1);

        // Verify that all input files exist
        foreach (string path in inputPaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Prepare streams for inputs and output
        var inputStreams = new List<Stream>();
        try
        {
            foreach (string path in inputPaths)
            {
                // Open each input PDF as a read‑only stream
                inputStreams.Add(new FileStream(path, FileMode.Open, FileAccess.Read));
            }

            // Create the output stream (will be overwritten if it exists)
            using (Stream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // PdfFileEditor does NOT implement IDisposable, so no using block for it
                PdfFileEditor editor = new PdfFileEditor
                {
                    // Automatically close all streams after concatenation
                    CloseConcatenatedStreams = true
                };

                // Concatenate all input streams into the output stream
                bool success = editor.Concatenate(inputStreams.ToArray(), outputStream);
                if (success)
                {
                    Console.WriteLine($"Successfully concatenated {inputPaths.Length} files into '{outputPath}'.");
                }
                else
                {
                    Console.Error.WriteLine("Concatenation failed.");
                }
            }
        }
        finally
        {
            // Ensure all input streams are disposed in case CloseConcatenatedStreams is false or an exception occurs
            foreach (var stream in inputStreams)
            {
                stream.Dispose();
            }
        }
    }
}