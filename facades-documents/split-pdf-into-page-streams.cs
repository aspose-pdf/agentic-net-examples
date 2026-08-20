using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream.
        using (FileStream sourceStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // PdfFileEditor does not implement IDisposable, so a plain instance is sufficient.
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into individual pages.
            // Each element of the returned array is a MemoryStream that contains a single‑page PDF.
            MemoryStream[] pageStreams = editor.SplitToPages(sourceStream);

            // Optional: write each page to a separate file and dispose the streams.
            for (int i = 0; i < pageStreams.Length; i++)
            {
                string outPath = $"page_{i + 1}.pdf";

                // Reset position before copying.
                pageStreams[i].Position = 0;

                using (FileStream outFile = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(outFile);
                }

                Console.WriteLine($"Saved page {i + 1} to {outPath}");

                // Release the memory used by the stream.
                pageStreams[i].Dispose();
            }
        }
    }
}