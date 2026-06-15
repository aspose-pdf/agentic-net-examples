using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";
        // Desired output file for the extracted front part
        const string outputPath = "output_front.pdf";
        // End page (inclusive) up to which pages will be extracted
        const int endPage = 5;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // Prepare an in‑memory stream to receive the split result
            using (MemoryStream outputStream = new MemoryStream())
            {
                // PdfFileEditor does NOT implement IDisposable, so instantiate directly
                PdfFileEditor editor = new PdfFileEditor();

                // Perform the split: pages 1 .. endPage are written to outputStream
                bool success = editor.SplitFromFirst(inputStream, endPage, outputStream);
                if (!success)
                {
                    Console.Error.WriteLine("SplitFromFirst operation failed.");
                    return;
                }

                // Reset the position of the memory stream before reading its contents
                outputStream.Position = 0;

                // Persist the in‑memory result to a physical file (optional)
                using (FileStream fileOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    outputStream.CopyTo(fileOut);
                }

                Console.WriteLine($"Pages 1‑{endPage} saved to '{outputPath}'.");
            }
        }
    }
}