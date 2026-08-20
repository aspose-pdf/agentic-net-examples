using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files – ensure they exist in the working directory
        const string inputFile1 = "file1.pdf";
        const string inputFile2 = "file2.pdf";

        if (!File.Exists(inputFile1) || !File.Exists(inputFile2))
        {
            Console.Error.WriteLine("Input PDF files not found.");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Concatenation using file paths
        // -----------------------------------------------------------------
        string outputPathPath = "concatenated_path.pdf";

        // Capture memory usage before the operation
        long beforePath = Process.GetCurrentProcess().PrivateMemorySize64;

        // PdfFileEditor does NOT implement IDisposable – do not use 'using'
        PdfFileEditor editorPath = new PdfFileEditor();
        // Close streams automatically after operation (not strictly needed here)
        editorPath.CloseConcatenatedStreams = true;

        // Perform concatenation using file paths
        bool successPath = editorPath.Concatenate(inputFile1, inputFile2, outputPathPath);

        // Capture memory usage after the operation
        long afterPath = Process.GetCurrentProcess().PrivateMemorySize64;

        Console.WriteLine($"Path overload success: {successPath}");
        Console.WriteLine($"Memory used (path overload): {(afterPath - beforePath) / 1024} KB");

        // -----------------------------------------------------------------
        // 2. Concatenation using streams
        // -----------------------------------------------------------------
        string outputPathStream = "concatenated_stream.pdf";

        // Open streams for the two input PDFs and the output PDF
        using (FileStream stream1 = new FileStream(inputFile1, FileMode.Open, FileAccess.Read))
        using (FileStream stream2 = new FileStream(inputFile2, FileMode.Open, FileAccess.Read))
        using (FileStream outStream = new FileStream(outputPathStream, FileMode.Create, FileAccess.Write))
        {
            // Capture memory usage before the operation
            long beforeStream = Process.GetCurrentProcess().PrivateMemorySize64;

            PdfFileEditor editorStream = new PdfFileEditor();
            editorStream.CloseConcatenatedStreams = true;

            // Perform concatenation using stream overloads
            bool successStream = editorStream.Concatenate(stream1, stream2, outStream);

            // Capture memory usage after the operation
            long afterStream = Process.GetCurrentProcess().PrivateMemorySize64;

            Console.WriteLine($"Stream overload success: {successStream}");
            Console.WriteLine($"Memory used (stream overload): {(afterStream - beforeStream) / 1024} KB");
        }

        // Note: PdfFileEditor does not need explicit disposal.
    }
}