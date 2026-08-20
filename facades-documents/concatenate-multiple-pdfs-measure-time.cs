using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Prepare list of 50 input PDF file names (e.g., input1.pdf … input50.pdf)
        const int fileCount = 50;
        string[] inputPaths = new string[fileCount];
        for (int i = 0; i < fileCount; i++)
        {
            inputPaths[i] = $"input{i + 1}.pdf";
        }

        // Open input streams
        Stream[] inputStreams = new Stream[fileCount];
        for (int i = 0; i < fileCount; i++)
        {
            if (!File.Exists(inputPaths[i]))
            {
                Console.Error.WriteLine($"File not found: {inputPaths[i]}");
                return;
            }
            inputStreams[i] = new FileStream(inputPaths[i], FileMode.Open, FileAccess.Read);
        }

        // Output stream (will be closed by the using statement)
        const string outputPath = "merged_output.pdf";
        using (Stream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Initialize PdfFileEditor and configure it to close input streams after concatenation
            PdfFileEditor editor = new PdfFileEditor
            {
                CloseConcatenatedStreams = true
            };

            // Measure execution time
            Stopwatch sw = Stopwatch.StartNew();
            bool success = editor.Concatenate(inputStreams, outputStream);
            sw.Stop();

            Console.WriteLine($"Concatenation {(success ? "succeeded" : "failed")} in {sw.ElapsedMilliseconds} ms.");
        }

        // At this point, all input streams have been closed automatically because
        // CloseConcatenatedStreams was set to true.
        // If for any reason they were not closed, dispose them manually:
        foreach (var stream in inputStreams)
        {
            stream?.Dispose();
        }
    }
}