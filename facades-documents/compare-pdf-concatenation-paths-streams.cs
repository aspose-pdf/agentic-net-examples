using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files (ensure they exist in the working directory)
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        string outputPathFile = "merged_path.pdf";
        string outputPathStream = "merged_stream.pdf";

        foreach (var f in inputFiles)
        {
            if (!File.Exists(f))
            {
                Console.Error.WriteLine($"Missing input file: {f}");
                return;
            }
        }

        // -------------------------------------------------
        // 1. Concatenation using file‑path overload
        // -------------------------------------------------
        GC.Collect();
        GC.WaitForPendingFinalizers();
        long memBeforePath = Process.GetCurrentProcess().PrivateMemorySize64;

        // PdfFileEditor does NOT implement IDisposable – no using block
        PdfFileEditor editorPath = new PdfFileEditor();
        editorPath.Concatenate(inputFiles, outputPathFile);

        GC.Collect();
        GC.WaitForPendingFinalizers();
        long memAfterPath = Process.GetCurrentProcess().PrivateMemorySize64;
        Console.WriteLine($"Path overload memory delta: {memAfterPath - memBeforePath} bytes");

        // -------------------------------------------------
        // 2. Concatenation using stream overload
        // -------------------------------------------------
        GC.Collect();
        GC.WaitForPendingFinalizers();
        long memBeforeStream = Process.GetCurrentProcess().PrivateMemorySize64;

        using (MemoryStream outputStream = new MemoryStream())
        {
            // Open each input file as a read‑only stream
            Stream[] inputStreams = new Stream[inputFiles.Length];
            try
            {
                for (int i = 0; i < inputFiles.Length; i++)
                {
                    inputStreams[i] = File.OpenRead(inputFiles[i]);
                }

                // Concatenate the streams into the output stream
                PdfFileEditor editorStream = new PdfFileEditor();
                editorStream.Concatenate(inputStreams, outputStream);

                // Persist the merged PDF produced in the memory stream to disk
                File.WriteAllBytes(outputPathStream, outputStream.ToArray());
            }
            finally
            {
                // Ensure all input streams are disposed
                foreach (var s in inputStreams)
                {
                    s?.Dispose();
                }
            }
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();
        long memAfterStream = Process.GetCurrentProcess().PrivateMemorySize64;
        Console.WriteLine($"Stream overload memory delta: {memAfterStream - memBeforeStream} bytes");
    }
}