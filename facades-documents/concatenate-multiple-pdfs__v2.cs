using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfConcatConsole
{
    class Program
    {
        /// <summary>
        /// Entry point for the console application.
        /// The program expects one or more file paths to PDF files as command‑line arguments.
        /// It concatenates the PDFs and writes the result to "concatenated.pdf" in the current directory.
        /// This mimics the behaviour of the original HTTP service, but is implemented as a pure console app
        /// because the project does not reference Microsoft.AspNetCore assemblies.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: AsposePdfConcatConsole <pdf1> <pdf2> [<pdf3> ...]");
                return;
            }

            // Validate that all supplied files exist and have a .pdf extension.
            var inputFiles = new List<string>();
            foreach (var path in args)
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine($"Error: File not found – {path}");
                    return;
                }
                if (!path.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Error: File is not a PDF – {path}");
                    return;
                }
                inputFiles.Add(path);
            }

            // Open each PDF as a read‑only stream. Streams are disposed in the finally block.
            var inputStreams = new List<Stream>();
            try
            {
                foreach (var file in inputFiles)
                {
                    // OpenRead returns a FileStream that must be disposed.
                    var stream = File.OpenRead(file);
                    inputStreams.Add(stream);
                }

                // Prepare an output stream that will hold the concatenated PDF.
                using var outputStream = new MemoryStream();

                // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
                var editor = new PdfFileEditor();

                // Concatenate the PDFs. The API expects an array of streams for the source PDFs.
                editor.Concatenate(inputStreams.ToArray(), outputStream);

                // Reset the position to the beginning before saving.
                outputStream.Position = 0;

                // Write the concatenated PDF to a file (simulating an HTTP response body).
                const string outputPath = "concatenated.pdf";
                using var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);
                outputStream.CopyTo(fileStream);

                Console.WriteLine($"Successfully concatenated {inputFiles.Count} PDF(s) to '{outputPath}'.");
            }
            finally
            {
                // Ensure all input streams are disposed to release file handles.
                foreach (var s in inputStreams)
                {
                    s.Dispose();
                }
            }
        }
    }
}
