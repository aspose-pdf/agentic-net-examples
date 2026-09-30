using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated – ensure they exist (create dummy PDFs if missing)
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        foreach (var file in inputFiles)
            EnsurePdfExists(file);

        // Output files for the two approaches
        const string outputPathFile   = "merged_file.pdf";
        const string outputPathStream = "merged_stream.pdf";

        // ------------------------------------------------------------
        // 1. Concatenation using file‑path overloads
        // ------------------------------------------------------------
        long memBeforeFile = GC.GetTotalMemory(true);

        // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using
        var editorFile = new PdfFileEditor();
        editorFile.Concatenate(inputFiles, outputPathFile);

        long memAfterFile = GC.GetTotalMemory(true);
        Console.WriteLine($"File‑path concatenation memory delta: {memAfterFile - memBeforeFile} bytes");

        // ------------------------------------------------------------
        // 2. Concatenation using stream overloads
        // ------------------------------------------------------------
        // Open a writable stream for the output PDF
        using (FileStream outputStream = new FileStream(outputPathStream, FileMode.Create, FileAccess.Write))
        {
            // Open read‑only streams for each input PDF
            Stream[] inputStreams = new Stream[inputFiles.Length];
            try
            {
                for (int i = 0; i < inputFiles.Length; i++)
                    inputStreams[i] = new FileStream(inputFiles[i], FileMode.Open, FileAccess.Read);

                long memBeforeStream = GC.GetTotalMemory(true);

                var editorStream = new PdfFileEditor();
                editorStream.Concatenate(inputStreams, outputStream);

                long memAfterStream = GC.GetTotalMemory(true);
                Console.WriteLine($"Stream concatenation memory delta: {memAfterStream - memBeforeStream} bytes");
            }
            finally
            {
                // Dispose all input streams even if an exception occurs
                foreach (var s in inputStreams)
                    s?.Dispose();
            }
        }
    }

    /// <summary>
    /// Creates a minimal one‑page PDF at <paramref name="path"/> if the file does not already exist.
    /// This prevents FileNotFoundException during the demo run.
    /// </summary>
    private static void EnsurePdfExists(string path)
    {
        if (File.Exists(path))
            return;

        // Create a simple PDF with a single blank page
        var doc = new Document();
        doc.Pages.Add();
        doc.Save(path);
    }
}
