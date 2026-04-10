using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two PDF files to concatenate
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: concat.exe <input1.pdf> <input2.pdf> [<input3.pdf> ...] <output.pdf>");
            return;
        }

        // The last argument is the output file, the rest are input files
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

        // Prepare input streams
        List<FileStream> inputStreams = new List<FileStream>();
        try
        {
            foreach (string path in inputPaths)
            {
                // Open each input PDF for reading
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                inputStreams.Add(fs);
            }

            // Open output stream for writing (create or overwrite)
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // PdfFileEditor does not implement IDisposable, so no using needed
                PdfFileEditor editor = new PdfFileEditor();

                // Optional: close streams automatically after operation
                editor.CloseConcatenatedStreams = true;

                // Concatenate all input streams into the output stream
                bool success = editor.Concatenate(inputStreams.ToArray(), outputStream);

                if (success)
                {
                    Console.WriteLine($"Successfully concatenated {inputStreams.Count} files into '{outputPath}'.");
                }
                else
                {
                    Console.Error.WriteLine("Concatenation failed.");
                }
            }
        }
        finally
        {
            // Ensure all input streams are closed even if an exception occurs
            foreach (var stream in inputStreams)
            {
                stream.Dispose();
            }
        }
    }
}