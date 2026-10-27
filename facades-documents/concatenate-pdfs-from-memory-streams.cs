using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file paths (replace with your actual sources)
        string[] inputPaths = { "first.pdf", "second.pdf", "third.pdf" };
        string outputPath = "merged.pdf";

        // Verify that all input files exist
        foreach (var path in inputPaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Load each PDF into a memory stream
        MemoryStream[] sourceStreams = new MemoryStream[inputPaths.Length];
        try
        {
            for (int i = 0; i < inputPaths.Length; i++)
            {
                byte[] data = File.ReadAllBytes(inputPaths[i]);
                sourceStreams[i] = new MemoryStream(data);
                sourceStreams[i].Position = 0; // Ensure stream is at the beginning
            }

            // Create the output file stream (no intermediate storage)
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // PdfFileEditor does NOT implement IDisposable; instantiate directly
                PdfFileEditor editor = new PdfFileEditor();

                // Concatenate the PDFs from memory streams into the output stream
                editor.Concatenate(sourceStreams, outputStream);
            }

            Console.WriteLine($"Successfully concatenated PDFs to '{outputPath}'.");
        }
        finally
        {
            // Dispose all memory streams
            foreach (var ms in sourceStreams)
            {
                ms?.Dispose();
            }
        }
    }
}