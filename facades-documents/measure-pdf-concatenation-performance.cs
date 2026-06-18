using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const int fileCount = 50;                     // number of PDFs to concatenate
        string inputFolder = "InputPdfs";             // folder containing the small PDFs
        string outputPath = "concatenated.pdf";       // result file

        // Prepare an array of input streams for the 50 PDFs
        Stream[] inputStreams = new Stream[fileCount];
        for (int i = 0; i < fileCount; i++)
        {
            string filePath = Path.Combine(inputFolder, $"file{i + 1}.pdf");
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"Missing input file: {filePath}");
                return;
            }

            // Open each PDF as a read‑only FileStream
            inputStreams[i] = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }

        // Create the output stream where the concatenated PDF will be written
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
            PdfFileEditor editor = new PdfFileEditor();

            // Instruct the editor to close the input streams after concatenation
            editor.CloseConcatenatedStreams = true;

            // Measure execution time
            Stopwatch sw = Stopwatch.StartNew();

            // Use the stream overload that accepts an array of input streams and one output stream
            bool success = editor.Concatenate(inputStreams, outputStream);

            sw.Stop();

            Console.WriteLine($"Concatenation succeeded: {success}");
            Console.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds} ms");
        }

        // Input streams have been closed by the editor because CloseConcatenatedStreams = true
    }
}