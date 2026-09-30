using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const int startPage = 3; // page from which to start splitting

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = File.OpenRead(inputPath))
        {
            // Prepare an in‑memory stream to receive the split result
            using (MemoryStream outputStream = new MemoryStream())
            {
                // PdfFileEditor does NOT implement IDisposable – instantiate directly
                PdfFileEditor editor = new PdfFileEditor();

                // Split from startPage to the end of the document, writing to outputStream.
                // NOTE: The correct overload expects (inputStream, startPage, outputStream).
                editor.SplitToEnd(inputStream, startPage, outputStream);

                // Reset the output stream position for further use
                outputStream.Position = 0;

                // Example: persist the in‑memory PDF to a file
                const string outputPath = "split_output.pdf";
                using (FileStream fileOut = File.Create(outputPath))
                {
                    outputStream.CopyTo(fileOut);
                }

                Console.WriteLine($"Pages {startPage}‑end saved to '{outputPath}'.");
            }
        }
    }
}
