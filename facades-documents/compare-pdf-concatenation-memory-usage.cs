using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Measures memory consumption of an action using GC.GetTotalMemory.
    static long MeasureMemory(Action action)
    {
        // Force a full garbage collection to get a clean baseline.
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long before = GC.GetTotalMemory(true);
        action();
        // Collect again after the operation.
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long after = GC.GetTotalMemory(true);
        return after - before;
    }

    static void Main()
    {
        const string inputFile1 = "file1.pdf";
        const string inputFile2 = "file2.pdf";
        const string outputPathFile = "concatenated_path.pdf";
        const string outputPathStream = "concatenated_stream.pdf";

        // Verify input files exist.
        if (!File.Exists(inputFile1) || !File.Exists(inputFile2))
        {
            Console.Error.WriteLine("Input PDF files not found.");
            return;
        }

        // -----------------------------------------------------------------
        // Concatenation using file‑path overload.
        // -----------------------------------------------------------------
        long memoryPath = MeasureMemory(() =>
        {
            PdfFileEditor editor = new PdfFileEditor {
                // Close streams automatically after operation (not strictly needed here).
                CloseConcatenatedStreams = true
            };
            // Concatenate two PDFs and write the result to a file.
            editor.Concatenate(inputFile1, inputFile2, outputPathFile);
        });

        Console.WriteLine($"Memory used (file‑path overload): {memoryPath:N0} bytes");

        // -----------------------------------------------------------------
        // Concatenation using stream overload.
        // -----------------------------------------------------------------
        long memoryStream = MeasureMemory(() =>
        {
            PdfFileEditor editor = new PdfFileEditor {
                CloseConcatenatedStreams = true
            };

            // Open input and output streams. Using statements ensure proper disposal.
            using (FileStream stream1 = new FileStream(inputFile1, FileMode.Open, FileAccess.Read))
            using (FileStream stream2 = new FileStream(inputFile2, FileMode.Open, FileAccess.Read))
            using (FileStream outStream = new FileStream(outputPathStream, FileMode.Create, FileAccess.Write))
            {
                // Concatenate using streams.
                editor.Concatenate(stream1, stream2, outStream);
            }
        });

        Console.WriteLine($"Memory used (stream overload): {memoryStream:N0} bytes");

        // -----------------------------------------------------------------
        // Optional: display process private memory for reference.
        // -----------------------------------------------------------------
        Process proc = Process.GetCurrentProcess();
        Console.WriteLine($"Process private memory after operations: {proc.PrivateMemorySize64:N0} bytes");
    }
}