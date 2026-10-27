using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two input PDF files and one output path.
        // Usage: dotnet run input1.pdf input2.pdf ... output.pdf
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <input1.pdf> <input2.pdf> ... <output.pdf>");
            return;
        }

        // The last argument is the output file; all preceding arguments are inputs.
        string outputPath = args[args.Length - 1];
        string[] inputPaths = new string[args.Length - 1];
        Array.Copy(args, inputPaths, args.Length - 1);

        // Validate input files exist.
        foreach (string path in inputPaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Prepare input streams.
        Stream[] inputStreams = new Stream[inputPaths.Length];
        try
        {
            for (int i = 0; i < inputPaths.Length; i++)
            {
                // Open each PDF file for reading.
                inputStreams[i] = new FileStream(inputPaths[i], FileMode.Open, FileAccess.Read);
            }

            // Open output stream for writing the concatenated PDF.
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // PdfFileEditor provides a Concatenate overload that works with streams.
                PdfFileEditor editor = new PdfFileEditor();
                editor.Concatenate(inputStreams, outputStream);
            }

            Console.WriteLine($"Successfully concatenated {inputPaths.Length} PDFs into '{outputPath}'.");
        }
        finally
        {
            // Ensure all input streams are closed even if an exception occurs.
            foreach (Stream s in inputStreams)
            {
                s?.Dispose();
            }
        }
    }
}