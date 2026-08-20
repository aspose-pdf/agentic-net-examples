using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // source PDF file
        const int endPage = 5;                  // split up to this page (inclusive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // MemoryStream will receive the front part of the PDF (pages 1..endPage)
            using (MemoryStream outputStream = new MemoryStream())
            {
                // PdfFileEditor is a Facades class; it does NOT implement IDisposable
                PdfFileEditor editor = new PdfFileEditor();

                // Perform the split operation in‑memory
                bool success = editor.SplitFromFirst(inputStream, endPage, outputStream);

                if (!success)
                {
                    Console.Error.WriteLine("SplitFromFirst operation failed.");
                    return;
                }

                // Reset the output stream position before reading from it
                outputStream.Position = 0;

                // Example: persist the in‑memory result to a file for verification
                const string outPath = "front_part.pdf";
                using (FileStream fileOut = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    outputStream.CopyTo(fileOut);
                }

                Console.WriteLine($"Pages 1‑{endPage} saved to '{outPath}'.");
            }
        }
    }
}