using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to source PDF files – replace with your actual sources or streams
        string[] sourcePdfPaths = { "first.pdf", "second.pdf", "third.pdf" };

        // Verify that all source files exist before proceeding
        foreach (var path in sourcePdfPaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"Source PDF not found: {Path.GetFullPath(path)}");
                return;
            }
        }

        // Output stream where the merged PDF will be written
        using (FileStream outputStream = new FileStream("merged.pdf", FileMode.Create, FileAccess.Write))
        {
            // Prepare an array of input streams corresponding to the source PDFs
            Stream[] inputStreams = new Stream[sourcePdfPaths.Length];
            try
            {
                for (int i = 0; i < sourcePdfPaths.Length; i++)
                {
                    inputStreams[i] = new FileStream(sourcePdfPaths[i], FileMode.Open, FileAccess.Read);
                }

                // PdfFileEditor does NOT implement IDisposable – do NOT wrap it in a using block
                PdfFileEditor editor = new PdfFileEditor();

                // Concatenate the input streams into the output stream
                editor.Concatenate(inputStreams, outputStream);
            }
            finally
            {
                // Ensure all input streams are properly disposed
                foreach (var stream in inputStreams)
                {
                    stream?.Dispose();
                }
            }
        }

        Console.WriteLine("PDF files have been merged into 'merged.pdf'.");
    }
}
