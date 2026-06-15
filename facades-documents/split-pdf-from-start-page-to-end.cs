using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF file
        const int startPage = 5;                       // page number to split from (1‑based)
        // Output will be kept in memory; optionally write to a file later
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using (FileStream sourceStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        // Create a memory stream to receive the rear part of the document
        using (MemoryStream outputStream = new MemoryStream())
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
            PdfFileEditor editor = new PdfFileEditor();

            // Split from the specified start page to the end of the document
            bool success = editor.SplitToEnd(sourceStream, startPage, outputStream);

            if (!success)
            {
                Console.Error.WriteLine("Split operation failed.");
                return;
            }

            // Reset the output stream position before reading or saving
            outputStream.Position = 0;

            // Example: write the resulting PDF to a file (optional)
            const string outputPath = "split_output.pdf";
            using (FileStream fileOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                outputStream.CopyTo(fileOut);
            }

            Console.WriteLine($"Successfully split PDF from page {startPage} to end. Output saved to '{outputPath}'.");
        }
    }
}