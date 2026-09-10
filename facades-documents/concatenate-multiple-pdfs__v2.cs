using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfConcatenationService
{
    class Program
    {
        static void Main(string[] args)
        {
            // Simple validation – at least one input PDF is required.
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: PdfConcatenationService <pdf1> <pdf2> ... [output]");
                return;
            }

            // Determine the output file name.
            // If the last argument ends with .pdf and there is more than one argument, treat it as the output path.
            // Otherwise default to "merged.pdf".
            string outputPath;
            int inputCount;
            if (args.Length > 1 && args[^1].EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                outputPath = args[^1];
                inputCount = args.Length - 1;
            }
            else
            {
                outputPath = "merged.pdf";
                inputCount = args.Length;
            }

            // Prepare an array of input streams.
            Stream[] inputStreams = new Stream[inputCount];
            try
            {
                for (int i = 0; i < inputCount; i++)
                {
                    string path = args[i];
                    if (!File.Exists(path))
                    {
                        Console.WriteLine($"File not found: {path}");
                        return;
                    }
                    // Open each PDF as a read‑only stream.
                    inputStreams[i] = File.OpenRead(path);
                }

                // Concatenate the PDFs using Aspose.Pdf.Facades.PdfFileEditor.
                using (MemoryStream outputStream = new MemoryStream())
                {
                    var editor = new PdfFileEditor
                    {
                        // Close the input streams automatically after concatenation.
                        CloseConcatenatedStreams = true
                    };

                    editor.Concatenate(inputStreams, outputStream);

                    // Write the merged PDF to the desired output file.
                    File.WriteAllBytes(outputPath, outputStream.ToArray());
                    Console.WriteLine($"Merged PDF saved to {outputPath}");
                }
            }
            finally
            {
                // Ensure all input streams are disposed in case of an exception or if CloseConcatenatedStreams is false.
                foreach (var s in inputStreams)
                {
                    s?.Dispose();
                }
            }
        }
    }
}
