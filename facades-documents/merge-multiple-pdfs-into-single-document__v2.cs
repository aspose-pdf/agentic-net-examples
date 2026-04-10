using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        string[] inputFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };
        // Destination merged PDF
        const string outputFile = "merged.pdf";

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Open input streams (read‑only) and the output stream (write)
        Stream[] inputStreams = new Stream[inputFiles.Length];
        try
        {
            for (int i = 0; i < inputFiles.Length; i++)
            {
                inputStreams[i] = new FileStream(inputFiles[i], FileMode.Open, FileAccess.Read);
            }

            using (FileStream outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
                PdfFileEditor editor = new PdfFileEditor();

                // Optional: let the editor close the input streams after concatenation
                editor.CloseConcatenatedStreams = true;

                // Concatenate the array of input streams into the output stream
                bool result = editor.Concatenate(inputStreams, outputStream);

                if (result)
                    Console.WriteLine($"Successfully merged PDFs into '{outputFile}'.");
                else
                    Console.Error.WriteLine("PDF concatenation failed.");
            }
        }
        finally
        {
            // Ensure all input streams are disposed in case the editor didn't close them
            foreach (Stream s in inputStreams)
            {
                s?.Dispose();
            }
        }
    }
}