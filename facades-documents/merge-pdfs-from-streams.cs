using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two input PDF file paths and one output path.
        // Example: PdfConcat input1.pdf input2.pdf output.pdf
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: PdfConcat <input1.pdf> <input2.pdf> [<input3.pdf> ...] <output.pdf>");
            return;
        }

        // The last argument is the output file, the rest are inputs.
        string outputPath = args[args.Length - 1];
        var inputPaths = new List<string>(args);
        inputPaths.RemoveAt(args.Length - 1);

        // Verify that every input file exists.
        foreach (var path in inputPaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Load each input PDF into a MemoryStream.
        var inputStreams = new List<Stream>();
        try
        {
            foreach (var path in inputPaths)
            {
                var ms = new MemoryStream(File.ReadAllBytes(path));
                inputStreams.Add(ms);
            }

            // Stream that will receive the concatenated PDF.
            using var outputStream = new MemoryStream();

            // PdfFileEditor does not implement IDisposable, but we can set it to close the streams after concatenation.
            var editor = new PdfFileEditor { CloseConcatenatedStreams = true };
            editor.Concatenate(inputStreams.ToArray(), outputStream);

            // Write the merged PDF to the requested output file.
            File.WriteAllBytes(outputPath, outputStream.ToArray());
            Console.WriteLine($"Merged PDF saved to: {outputPath}");
        }
        finally
        {
            // Ensure any streams that were not closed by the editor are disposed.
            foreach (var s in inputStreams)
                s?.Dispose();
        }
    }
}
