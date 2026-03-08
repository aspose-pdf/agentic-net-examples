using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        string[] inputFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };

        // Verify that all input files exist
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        // Open each input file as a read‑only stream
        Stream[] inputStreams = new Stream[inputFiles.Length];
        for (int i = 0; i < inputFiles.Length; i++)
        {
            inputStreams[i] = new FileStream(inputFiles[i], FileMode.Open, FileAccess.Read);
        }

        // Prepare an in‑memory stream that will hold the concatenated result
        using (MemoryStream outputStream = new MemoryStream())
        {
            // PdfFileEditor performs the concatenation without needing a Document instance
            PdfFileEditor editor = new PdfFileEditor();

            // Ensure the editor closes the input streams after the operation
            editor.CloseConcatenatedStreams = true;

            // Concatenate the input streams into the output stream
            bool succeeded = editor.Concatenate(inputStreams, outputStream);
            if (!succeeded)
            {
                Console.Error.WriteLine("Concatenation failed.");
                return;
            }

            // Reset the position so the stream can be read from the beginning
            outputStream.Position = 0;

            // Example usage: write the in‑memory PDF to a physical file
            File.WriteAllBytes("merged_output.pdf", outputStream.ToArray());

            Console.WriteLine("PDF files concatenated successfully.");
        }

        // Input streams have been closed by the editor because CloseConcatenatedStreams = true
    }
}