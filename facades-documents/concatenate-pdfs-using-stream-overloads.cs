using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two input PDF files
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: ConcatenatePdf <input1.pdf> <input2.pdf> [<input3.pdf> ...]");
            return;
        }

        // Output file name (can be changed as needed)
        const string outputPath = "output.pdf";

        // Prepare a list to hold the input streams
        List<Stream> inputStreams = new List<Stream>();

        try
        {
            // Open each input PDF as a read‑only FileStream
            foreach (string inputPath in args)
            {
                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"File not found: {inputPath}");
                    // Clean up any streams already opened
                    foreach (var s in inputStreams) s.Dispose();
                    return;
                }

                // FileAccess.Read ensures the stream can be used for concatenation
                FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
                inputStreams.Add(fs);
            }

            // Create the output stream (will be overwritten if it exists)
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // PdfFileEditor provides the Concatenate overload that works with streams
                PdfFileEditor editor = new PdfFileEditor();

                // Optional: close the input streams automatically after concatenation
                editor.CloseConcatenatedStreams = true;

                // Perform concatenation
                bool success = editor.Concatenate(inputStreams.ToArray(), outputStream);

                if (success)
                {
                    Console.WriteLine($"Successfully concatenated {args.Length} PDFs into '{outputPath}'.");
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
            foreach (var s in inputStreams)
            {
                s.Dispose();
            }
        }
    }
}