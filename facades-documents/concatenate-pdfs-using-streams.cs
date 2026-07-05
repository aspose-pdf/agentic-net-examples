using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two input PDFs and one output PDF.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: concat <input1.pdf> <input2.pdf> [<inputN.pdf> ...] <output.pdf>");
            return;
        }

        // The last argument is the output file, the rest are input files.
        string outputPath = args[args.Length - 1];
        string[] inputPaths = new string[args.Length - 1];
        Array.Copy(args, inputPaths, args.Length - 1);

        // Verify that all input files exist.
        foreach (string path in inputPaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Prepare input streams.
        var inputStreams = new List<Stream>();
        try
        {
            foreach (string path in inputPaths)
            {
                // Open each input PDF for reading.
                Stream s = new FileStream(path, FileMode.Open, FileAccess.Read);
                inputStreams.Add(s);
            }

            // Open the output stream for writing.
            using (Stream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // PdfFileEditor does not implement IDisposable, so we instantiate directly.
                PdfFileEditor editor = new PdfFileEditor();

                // Optional: close the input streams automatically after concatenation.
                editor.CloseConcatenatedStreams = true;

                // Perform concatenation using the stream overload.
                bool success = editor.Concatenate(inputStreams.ToArray(), outputStream);

                if (success)
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
            // Ensure all input streams are closed if CloseConcatenatedStreams was not set or an exception occurred.
            foreach (Stream s in inputStreams)
            {
                s?.Dispose();
            }
        }
    }
}