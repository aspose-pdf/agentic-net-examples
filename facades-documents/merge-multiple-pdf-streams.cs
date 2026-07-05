using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        const string outputFile = "merged.pdf";

        // ---------------------------------------------------------------------
        // Ensure that the input files exist.  For a self‑contained example we
        // create simple one‑page PDFs on the fly if they are missing.
        // ---------------------------------------------------------------------
        foreach (var path in inputFiles)
        {
            if (!File.Exists(path))
            {
                // Create a minimal PDF with a single blank page
                using (var doc = new Document())
                {
                    doc.Pages.Add();
                    doc.Save(path);
                }
            }
        }

        // Open each input file as a read‑only stream
        Stream[] inputStreams = new Stream[inputFiles.Length];
        for (int i = 0; i < inputFiles.Length; i++)
        {
            inputStreams[i] = new FileStream(inputFiles[i], FileMode.Open, FileAccess.Read);
        }

        // Create the output stream (write‑only)
        using (FileStream outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does NOT implement IDisposable; instantiate directly
            PdfFileEditor editor = new PdfFileEditor();

            // Automatically close input streams after concatenation (optional)
            editor.CloseConcatenatedStreams = true;

            // Concatenate all input streams into the output stream
            bool result = editor.Concatenate(inputStreams, outputStream);

            Console.WriteLine(result ? "PDF streams merged successfully." : "Failed to merge PDF streams.");
        }

        // If CloseConcatenatedStreams was set to false, ensure remaining streams are closed.
        // (In this example it is true, but the code is kept for completeness.)
        foreach (var stream in inputStreams)
        {
            if (stream != null && stream.CanRead)
            {
                stream.Dispose();
            }
        }
    }
}
