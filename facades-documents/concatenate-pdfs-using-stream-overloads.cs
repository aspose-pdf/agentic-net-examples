using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two PDF files to concatenate
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: ConcatenatePdf <input1.pdf> <input2.pdf> [more... ]");
            return;
        }

        // Verify that all input files exist
        foreach (string path in args)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"File not found: {path}");
                return;
            }
        }

        // Prepare a list to hold the opened input streams
        List<Stream> inputStreams = new List<Stream>();
        try
        {
            // Open each input PDF as a read‑only FileStream
            foreach (string path in args)
            {
                inputStreams.Add(File.OpenRead(path));
            }

            // Create the output stream (will be overwritten if it exists)
            using (FileStream outputStream = new FileStream("merged.pdf", FileMode.Create, FileAccess.Write))
            {
                // PdfFileEditor does NOT implement IDisposable, so we do NOT wrap it in a using block
                PdfFileEditor editor = new PdfFileEditor();

                // Concatenate using the stream overloads
                editor.Concatenate(inputStreams.ToArray(), outputStream);
            }

            Console.WriteLine("PDF files successfully concatenated to 'merged.pdf'.");
        }
        finally
        {
            // Ensure all input streams are properly disposed
            foreach (Stream s in inputStreams)
            {
                s.Dispose();
            }
        }
    }
}