using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfConcatService
{
    class Program
    {
        /// <summary>
        /// Concatenates multiple PDF files into a single PDF.
        /// Usage: PdfConcatService <output.pdf> <input1.pdf> [<input2.pdf> ...]
        /// </summary>
        static void Main(string[] args)
        {
            // Validate arguments – at least one output path and one input PDF are required.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: PdfConcatService <output.pdf> <input1.pdf> [<input2.pdf> ...]");
                return;
            }

            string outputPath = args[0];
            var inputPaths = new List<string>(args).GetRange(1, args.Length - 1);

            // Prepare a list to hold the opened file streams for each input PDF.
            var inputStreams = new List<Stream>();
            try
            {
                foreach (var path in inputPaths)
                {
                    if (!File.Exists(path))
                    {
                        Console.WriteLine($"File not found: {path}");
                        return;
                    }
                    // Open each PDF as a read‑only FileStream.
                    var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    inputStreams.Add(fs);
                }

                // Concatenate the PDFs using Aspose.Pdf.Facades.PdfFileEditor.
                using (var outputStream = new MemoryStream())
                {
                    var editor = new PdfFileEditor
                    {
                        // Instruct the editor to close the input streams after concatenation.
                        CloseConcatenatedStreams = true
                    };

                    editor.Concatenate(inputStreams.ToArray(), outputStream);

                    // Write the resulting PDF to the requested output file.
                    File.WriteAllBytes(outputPath, outputStream.ToArray());
                    Console.WriteLine($"Concatenated PDF saved to {outputPath}");
                }
            }
            finally
            {
                // Ensure every opened stream is disposed, even if an exception occurs.
                foreach (var s in inputStreams)
                {
                    s.Dispose();
                }
            }
        }
    }
}
