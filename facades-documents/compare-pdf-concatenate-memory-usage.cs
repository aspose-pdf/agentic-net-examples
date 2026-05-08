using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string file1 = "file1.pdf";
        const string file2 = "file2.pdf";
        const string outputPath = "concatenated_path.pdf";
        const string outputStreamPath = "concatenated_stream.pdf";

        // Ensure input files exist
        if (!File.Exists(file1) || !File.Exists(file2))
        {
            Console.Error.WriteLine("Input PDF files not found.");
            return;
        }

        // Measure memory for path‑based concatenation
        long beforePath = GetCurrentMemory();
        bool pathResult = ConcatenateUsingPaths(file1, file2, outputPath);
        long afterPath = GetCurrentMemory();

        // Measure memory for stream‑based concatenation
        long beforeStream = GetCurrentMemory();
        bool streamResult = ConcatenateUsingStreams(file1, file2, outputStreamPath);
        long afterStream = GetCurrentMemory();

        Console.WriteLine($"Path overload success: {pathResult}");
        Console.WriteLine($"Memory before path: {FormatBytes(beforePath)}");
        Console.WriteLine($"Memory after  path: {FormatBytes(afterPath)}");
        Console.WriteLine($"Increase (path): {FormatBytes(afterPath - beforePath)}");
        Console.WriteLine();
        Console.WriteLine($"Stream overload success: {streamResult}");
        Console.WriteLine($"Memory before stream: {FormatBytes(beforeStream)}");
        Console.WriteLine($"Memory after  stream: {FormatBytes(afterStream)}");
        Console.WriteLine($"Increase (stream): {FormatBytes(afterStream - beforeStream)}");
    }

    // Concatenates two PDFs using file paths
    static bool ConcatenateUsingPaths(string firstFile, string secondFile, string outputFile)
    {
        // PdfFileEditor does not implement IDisposable, so no using block is required
        PdfFileEditor editor = new PdfFileEditor();
        // CloseConcatenatedStreams is irrelevant for path overload but set for consistency
        editor.CloseConcatenatedStreams = true;
        return editor.Concatenate(firstFile, secondFile, outputFile);
    }

    // Concatenates two PDFs using streams
    static bool ConcatenateUsingStreams(string firstFile, string secondFile, string outputFile)
    {
        // Open input streams in read mode and output stream in write mode
        using (FileStream stream1 = new FileStream(firstFile, FileMode.Open, FileAccess.Read, FileShare.Read))
        using (FileStream stream2 = new FileStream(secondFile, FileMode.Open, FileAccess.Read, FileShare.Read))
        using (FileStream outStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            PdfFileEditor editor = new PdfFileEditor
            {
                // Ensure streams are closed after the operation to free memory
                CloseConcatenatedStreams = true
            };
            return editor.Concatenate(stream1, stream2, outStream);
        }
    }

    // Retrieves the current process private memory size (in bytes)
    static long GetCurrentMemory()
    {
        Process proc = Process.GetCurrentProcess();
        proc.Refresh();
        return proc.PrivateMemorySize64;
    }

    // Formats byte count into a human‑readable string
    static string FormatBytes(long bytes)
    {
        const long KB = 1024;
        const long MB = KB * 1024;
        const long GB = MB * 1024;

        if (bytes >= GB) return $"{bytes / (double)GB:F2} GB";
        if (bytes >= MB) return $"{bytes / (double)MB:F2} MB";
        if (bytes >= KB) return $"{bytes / (double)KB:F2} KB";
        return $"{bytes} B";
    }
}