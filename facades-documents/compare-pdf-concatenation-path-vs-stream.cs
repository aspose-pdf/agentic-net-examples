using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files – adjust paths as needed
        const string inputFile1 = "file1.pdf";
        const string inputFile2 = "file2.pdf";

        // Output files for each overload
        const string outputPathConcat = "concatenated_path.pdf";
        const string outputStreamConcat = "concatenated_stream.pdf";

        // Verify input files exist
        if (!File.Exists(inputFile1) || !File.Exists(inputFile2))
        {
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Concatenation using file‑path overload
        // -----------------------------------------------------------------
        long beforePath = GC.GetTotalMemory(true);
        PdfFileEditor editorPath = new PdfFileEditor();
        // Optional: close streams automatically after operation
        editorPath.CloseConcatenatedStreams = true;
        bool successPath = editorPath.Concatenate(inputFile1, inputFile2, outputPathConcat);
        long afterPath = GC.GetTotalMemory(true);
        long memoryUsedPath = afterPath - beforePath;

        Console.WriteLine($"Path overload success: {successPath}");
        Console.WriteLine($"Memory used (path overload): {memoryUsedPath:N0} bytes");

        // -----------------------------------------------------------------
        // 2. Concatenation using stream overload
        // -----------------------------------------------------------------
        long beforeStream = GC.GetTotalMemory(true);
        PdfFileEditor editorStream = new PdfFileEditor();
        editorStream.CloseConcatenatedStreams = true;

        using (FileStream stream1 = new FileStream(inputFile1, FileMode.Open, FileAccess.Read))
        using (FileStream stream2 = new FileStream(inputFile2, FileMode.Open, FileAccess.Read))
        using (FileStream outStream = new FileStream(outputStreamConcat, FileMode.Create, FileAccess.Write))
        {
            bool successStream = editorStream.Concatenate(stream1, stream2, outStream);
            long afterStream = GC.GetTotalMemory(true);
            long memoryUsedStream = afterStream - beforeStream;

            Console.WriteLine($"Stream overload success: {successStream}");
            Console.WriteLine($"Memory used (stream overload): {memoryUsedStream:N0} bytes");
        }

        // -----------------------------------------------------------------
        // Summary
        // -----------------------------------------------------------------
        Console.WriteLine("Comparison complete.");
    }
}