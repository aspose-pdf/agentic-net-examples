using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two input PDF file paths. An optional last argument can be the output file name.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfConcatenationService <input1.pdf> <input2.pdf> [<input3.pdf> ...] [output.pdf]");
            return;
        }

        // Determine output path. If the last argument ends with .pdf and there are more than two arguments, treat it as the output file.
        string outputPath;
        int inputCount;
        if (args.Length > 2 && args[^1].EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            outputPath = args[^1];
            inputCount = args.Length - 1;
        }
        else
        {
            outputPath = "merged.pdf";
            inputCount = args.Length;
        }

        // Open a read stream for each input file.
        Stream[] inputStreams = new Stream[inputCount];
        try
        {
            for (int i = 0; i < inputCount; i++)
            {
                inputStreams[i] = File.OpenRead(args[i]);
            }

            // MemoryStream that will receive the concatenated PDF.
            using (MemoryStream outputStream = new MemoryStream())
            {
                // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
                PdfFileEditor editor = new PdfFileEditor
                {
                    // Close the input streams automatically after concatenation.
                    CloseConcatenatedStreams = true
                };

                // Perform concatenation using the stream overload.
                editor.Concatenate(inputStreams, outputStream);

                // Write the merged PDF to the desired output file.
                File.WriteAllBytes(outputPath, outputStream.ToArray());
                Console.WriteLine($"Successfully merged {inputCount} PDFs into '{outputPath}'.");
            }
        }
        finally
        {
            // Ensure all input streams are disposed in case CloseConcatenatedStreams is false or an exception occurs.
            foreach (var s in inputStreams)
            {
                s?.Dispose();
            }
        }
    }
}
