using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfConcatConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Expected usage: PdfConcatConsole <output.pdf> <input1.pdf> [<input2.pdf> ...]
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: PdfConcatConsole <output.pdf> <input1.pdf> [<input2.pdf> ...]");
                return;
            }

            string outputPath = args[0];
            var inputPaths = new List<string>(args);
            inputPaths.RemoveAt(0); // first argument is the output file

            // Verify that every input file exists before processing
            foreach (var path in inputPaths)
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine($"Error: File not found - {path}");
                    return;
                }
            }

            // Load each PDF into a memory stream – this mimics receiving streams over HTTP
            var inputStreams = new List<Stream>();
            try
            {
                foreach (var path in inputPaths)
                {
                    var ms = new MemoryStream(File.ReadAllBytes(path));
                    inputStreams.Add(ms);
                }

                // Output stream that will contain the merged PDF
                using (var outputStream = new MemoryStream())
                {
                    var editor = new PdfFileEditor
                    {
                        // When true, PdfFileEditor will close the input streams after concatenation
                        CloseConcatenatedStreams = true
                    };

                    // Perform the concatenation
                    editor.Concatenate(inputStreams.ToArray(), outputStream);

                    // Persist the merged PDF to the requested output file
                    File.WriteAllBytes(outputPath, outputStream.ToArray());
                }

                Console.WriteLine($"Successfully concatenated {inputStreams.Count} PDFs into '{outputPath}'.");
            }
            finally
            {
                // Ensure all streams are disposed even if an exception occurs
                foreach (var s in inputStreams)
                {
                    s.Dispose();
                }
            }
        }
    }
}
