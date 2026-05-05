using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF file
        const string outputPath = "output_front.pdf";   // where the split part will be saved
        const int    endPage    = 5;                    // split up to this page (inclusive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // Use a MemoryStream to keep the result in memory
            using (MemoryStream outputStream = new MemoryStream())
            {
                // PdfFileEditor does NOT implement IDisposable – instantiate directly
                PdfFileEditor editor = new PdfFileEditor();

                // Split from the first page up to 'endPage' and write the front part to outputStream
                bool succeeded = editor.SplitFromFirst(inputStream, endPage, outputStream);
                if (!succeeded)
                {
                    Console.Error.WriteLine("SplitFromFirst operation failed.");
                    return;
                }

                // Reset the position before reading the stream
                outputStream.Position = 0;

                // Persist the in‑memory result to a file (optional)
                using (FileStream fileOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    outputStream.CopyTo(fileOut);
                }

                Console.WriteLine($"Front part up to page {endPage} saved to '{outputPath}'.");
            }
        }
    }
}