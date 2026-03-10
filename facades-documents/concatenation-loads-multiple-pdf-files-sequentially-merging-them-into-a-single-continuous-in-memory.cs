using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to concatenate
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };

        // Verify that all input files exist
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Open each file as a read‑only stream
        Stream[] inputStreams = new Stream[inputFiles.Length];
        try
        {
            for (int i = 0; i < inputFiles.Length; i++)
            {
                inputStreams[i] = new FileStream(inputFiles[i], FileMode.Open, FileAccess.Read);
            }

            // Prepare an in‑memory stream for the concatenated result
            using (MemoryStream outputStream = new MemoryStream())
            {
                PdfFileEditor editor = new PdfFileEditor();

                // Close the source streams automatically after concatenation
                editor.CloseConcatenatedStreams = true;

                // Perform concatenation
                bool ok = editor.Concatenate(inputStreams, outputStream);
                if (!ok)
                {
                    Console.Error.WriteLine("Concatenation failed.");
                    return;
                }

                // Reset the output stream position for further processing
                outputStream.Position = 0;

                // Example: write the merged PDF to disk
                File.WriteAllBytes("merged.pdf", outputStream.ToArray());
                Console.WriteLine("PDF files concatenated successfully into merged.pdf");
            }
        }
        finally
        {
            // Ensure any remaining streams are disposed
            foreach (var s in inputStreams)
            {
                s?.Dispose();
            }
        }
    }
}