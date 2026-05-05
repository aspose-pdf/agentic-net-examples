using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths of the source PDF files (could be any source; here we read from disk)
        string[] inputFiles = { "first.pdf", "second.pdf", "third.pdf" };
        string outputFile = "merged.pdf";

        // Verify that all input files exist
        foreach (var path in inputFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Load each PDF into a MemoryStream (in‑memory representation)
        MemoryStream[] inputStreams = new MemoryStream[inputFiles.Length];
        try
        {
            for (int i = 0; i < inputFiles.Length; i++)
            {
                byte[] data = File.ReadAllBytes(inputFiles[i]);
                inputStreams[i] = new MemoryStream(data);
                inputStreams[i].Position = 0; // Ensure the stream is at the beginning
            }

            // Create the output file stream where the concatenated PDF will be written
            using (FileStream outStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                PdfFileEditor editor = new PdfFileEditor();

                // When true, the editor will close the input streams after concatenation
                editor.CloseConcatenatedStreams = true;

                // Concatenate the in‑memory streams directly into the output stream
                bool success = editor.TryConcatenate(inputStreams, outStream);

                if (success)
                {
                    Console.WriteLine($"Successfully concatenated PDFs to '{outputFile}'.");
                }
                else
                {
                    Console.Error.WriteLine("PDF concatenation failed.");
                }
            }
        }
        finally
        {
            // Dispose all MemoryStreams to release resources
            foreach (var ms in inputStreams)
            {
                ms?.Dispose();
            }
        }
    }
}