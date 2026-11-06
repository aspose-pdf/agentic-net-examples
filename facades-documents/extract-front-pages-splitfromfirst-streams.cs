using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF
        const string outputPath = "extracted.pdf"; // where to save the extracted pages
        const int    endPage    = 5;                // extract pages 1 through 5

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = File.OpenRead(inputPath))
        // Prepare an in‑memory stream to receive the extracted pages
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Facade class for file‑level operations (no IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // Extract pages from the first page up to 'endPage' into the output stream
            // Use the overload that works with streams: (inputStream, endPage, outputStream)
            bool success = editor.SplitFromFirst(inputStream, endPage, outputStream);
            if (!success)
            {
                Console.Error.WriteLine("Failed to split the PDF.");
                return;
            }

            // Reset the position of the memory stream before reading/writing it
            outputStream.Position = 0;

            // Persist the extracted PDF to disk (optional – you could also return the byte[])
            using (FileStream fileOut = File.Create(outputPath))
            {
                outputStream.CopyTo(fileOut);
            }
        }

        Console.WriteLine($"Pages 1‑{endPage} extracted to '{outputPath}'.");
    }
}
