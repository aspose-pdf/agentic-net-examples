using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Simple console‑based PDF concatenation utility.
        // Usage: PdfConcatenationService <input1.pdf> <input2.pdf> ... [output.pdf]
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: PdfConcatenationService <pdf1> <pdf2> ... [output]");
            return;
        }

        // Determine output file name. If the last argument ends with .pdf and there is more than one argument,
        // treat it as the desired output path; otherwise default to "merged.pdf".
        string outputPath = "merged.pdf";
        var inputFiles = new List<string>(args);
        if (args.Length > 1 && args[args.Length - 1].EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            outputPath = args[args.Length - 1];
            inputFiles.RemoveAt(args.Length - 1);
        }

        // Validate each input file.
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"Error: File not found - {file}");
                return;
            }
            if (!file.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Error: Not a PDF file - {file}");
                return;
            }
        }

        var inputStreams = new List<Stream>();
        try
        {
            // Open each PDF as a read‑only stream.
            foreach (var file in inputFiles)
            {
                inputStreams.Add(File.OpenRead(file));
            }

            // MemoryStream that will receive the concatenated result.
            using var outputStream = new MemoryStream();

            var editor = new PdfFileEditor();
            // Instruct the editor to close the input streams after concatenation.
            editor.CloseConcatenatedStreams = true;
            editor.Concatenate(inputStreams.ToArray(), outputStream);

            // Reset position before reading.
            outputStream.Position = 0;

            // Write the merged PDF to the desired output file.
            using var fileOut = File.Create(outputPath);
            outputStream.CopyTo(fileOut);

            Console.WriteLine($"Merged PDF saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during concatenation: {ex.Message}");
        }
        finally
        {
            // Ensure all input streams are disposed.
            foreach (var s in inputStreams)
            {
                s.Dispose();
            }
        }
    }
}
