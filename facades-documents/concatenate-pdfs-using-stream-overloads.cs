using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // At least two PDF files are required.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: concatpdf <input1.pdf> <input2.pdf> [<input3.pdf> ...] [<output.pdf>]");
            return;
        }

        // Determine output file. If the last argument looks like an output path (ends with .pdf) and is not an existing input file, treat it as output.
        string outputPath;
        List<string> inputPaths = new List<string>();
        if (args.Length > 2 && args[args.Length - 1].EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            string possibleOutput = args[args.Length - 1];
            bool isExistingInput = false;
            foreach (var a in args)
            {
                if (string.Equals(a, possibleOutput, StringComparison.OrdinalIgnoreCase) && File.Exists(a))
                {
                    isExistingInput = true;
                    break;
                }
            }

            if (!isExistingInput)
            {
                outputPath = possibleOutput;
                for (int i = 0; i < args.Length - 1; i++)
                    inputPaths.Add(args[i]);
            }
            else
            {
                outputPath = "merged.pdf";
                foreach (var a in args)
                    inputPaths.Add(a);
            }
        }
        else
        {
            outputPath = "merged.pdf";
            foreach (var a in args)
                inputPaths.Add(a);
        }

        // Verify that all input files exist.
        foreach (var path in inputPaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Open input streams.
        List<Stream> inputStreams = new List<Stream>();
        try
        {
            foreach (var path in inputPaths)
                inputStreams.Add(new FileStream(path, FileMode.Open, FileAccess.Read));

            // Open output stream.
            using (Stream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                PdfFileEditor editor = new PdfFileEditor();
                // Let the editor close the streams after concatenation.
                editor.CloseConcatenatedStreams = true;

                bool success = editor.Concatenate(inputStreams.ToArray(), outputStream);
                if (success)
                    Console.WriteLine($"Successfully concatenated {inputStreams.Count} PDFs into '{outputPath}'.");
                else
                    Console.Error.WriteLine("Concatenation failed.");
            }
        }
        finally
        {
            // Ensure any streams not closed by the editor are disposed.
            foreach (var s in inputStreams)
                s?.Dispose();
        }
    }
}