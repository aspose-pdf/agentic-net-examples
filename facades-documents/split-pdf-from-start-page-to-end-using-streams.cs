using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Page number to start splitting from (1‑based indexing)
        const int startPage = 3;
        // Output PDF file path (result of the split)
        const string outputPath = "split_from_page3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Open the source PDF as a read‑only stream
            using (FileStream inputStream = File.OpenRead(inputPath))
            // Prepare an in‑memory stream to receive the split result
            using (MemoryStream outputStream = new MemoryStream())
            {
                // PdfFileEditor does NOT implement IDisposable, so no using block for it
                PdfFileEditor editor = new PdfFileEditor();

                // Split from the specified start page to the end of the document.
                // The method works with streams, keeping the operation fully in memory.
                editor.SplitToEnd(inputStream, startPage, outputStream);

                // Ensure the output stream is positioned at the beginning before reading
                outputStream.Position = 0;

                // Persist the in‑memory PDF to a file (optional – you could also return the stream)
                using (FileStream fileOut = File.Create(outputPath))
                {
                    outputStream.CopyTo(fileOut);
                }

                Console.WriteLine($"Successfully split PDF from page {startPage} to end. Saved as '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}