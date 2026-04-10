using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "input.pdf";
        const int startPage = 5; // 1‑based page number to start splitting from

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
        {
            // MemoryStream will receive the rear part of the document
            using (MemoryStream outputStream = new MemoryStream())
            {
                PdfFileEditor editor = new PdfFileEditor();

                // Split from the specified page to the end of the document
                bool success = editor.SplitToEnd(inputStream, startPage, outputStream);
                if (!success)
                {
                    Console.Error.WriteLine("SplitToEnd operation failed.");
                    return;
                }

                // Reset the output stream position before reading from it
                outputStream.Position = 0;

                // Example: write the split part to a physical file
                const string outPath = "split_part.pdf";
                using (FileStream fileOut = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    outputStream.CopyTo(fileOut);
                }

                Console.WriteLine($"Split part saved to '{outPath}'.");
            }
        }
    }
}