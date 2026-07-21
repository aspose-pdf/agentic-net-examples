using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    // Helper to get current process memory usage (in bytes)
    static long GetMemoryUsage()
    {
        // Force a full garbage collection to get a more accurate measurement
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        return Process.GetCurrentProcess().PrivateMemorySize64;
    }

    // Creates a minimal PDF file with a single page containing the supplied text.
    static void CreateSamplePdf(string path, string text)
    {
        using (Document doc = new Document())
        {
            // Add a blank page
            doc.Pages.Add();
            // Add simple text so the file is not empty
            TextFragment tf = new TextFragment(text);
            doc.Pages[1].Paragraphs.Add(tf);
            // Save to the requested location
            doc.Save(path);
        }
    }

    static void Main()
    {
        // -----------------------------------------------------------------
        // Prepare sample input PDFs (the sandbox starts empty)
        // -----------------------------------------------------------------
        const string firstPdfPath  = "file1.pdf";
        const string secondPdfPath = "file2.pdf";

        CreateSamplePdf(firstPdfPath,  "First PDF – page 1");
        CreateSamplePdf(secondPdfPath, "Second PDF – page 1");

        // Output files for each overload
        const string outputPathFile   = "concatenated_file.pdf";
        const string outputPathStream = "concatenated_stream.pdf";

        // -----------------------------------------------------------------
        // 1. Concatenation using file‑path overload
        // -----------------------------------------------------------------
        long memBeforeFile = GetMemoryUsage();

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly
        PdfFileEditor editorFile = new PdfFileEditor();
        // Optional: close streams automatically after operation (not needed for file paths)
        editorFile.CloseConcatenatedStreams = true;

        // Perform concatenation using file paths
        bool successFile = editorFile.Concatenate(firstPdfPath, secondPdfPath, outputPathFile);

        long memAfterFile = GetMemoryUsage();

        Console.WriteLine($"File‑path Concatenation success: {successFile}");
        Console.WriteLine($"Memory before (file‑path): {memBeforeFile:N0} bytes");
        Console.WriteLine($"Memory after  (file‑path): {memAfterFile:N0} bytes");
        Console.WriteLine($"Memory delta (file‑path): {memAfterFile - memBeforeFile:N0} bytes");
        Console.WriteLine();

        // -----------------------------------------------------------------
        // 2. Concatenation using stream overload
        // -----------------------------------------------------------------
        long memBeforeStream = GetMemoryUsage();

        // Open input streams in read mode and output stream in write mode
        using (FileStream stream1 = new FileStream(firstPdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream stream2 = new FileStream(secondPdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream outStream = new FileStream(outputPathStream, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor editorStream = new PdfFileEditor();
            // Ensure streams are closed after concatenation to free resources promptly
            editorStream.CloseConcatenatedStreams = true;

            // Perform concatenation using streams
            bool successStream = editorStream.Concatenate(stream1, stream2, outStream);

            Console.WriteLine($"Stream Concatenation success: {successStream}");
        }

        long memAfterStream = GetMemoryUsage();

        Console.WriteLine($"Memory before (stream): {memBeforeStream:N0} bytes");
        Console.WriteLine($"Memory after  (stream): {memAfterStream:N0} bytes");
        Console.WriteLine($"Memory delta (stream): {memAfterStream - memBeforeStream:N0} bytes");
    }
}
