using System;
using System.IO;
using Aspose.Pdf;               // Added to resolve PageSize reference (if any)
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";

        // Page number from which the split should start (1‑based indexing)
        const int startPage = 5;

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create an instance of PdfFileEditor (no IDisposable implementation)
        PdfFileEditor editor = new PdfFileEditor();

        // Perform the split using in‑memory streams
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Split from the specified start page to the end of the document
            bool success = editor.SplitToEnd(inputStream, startPage, outputStream);

            if (!success)
            {
                Console.Error.WriteLine("Split operation failed.");
                return;
            }

            // Reset the output stream position before further processing
            outputStream.Position = 0;

            // Optional: write the resulting PDF to a physical file
            const string outputPath = "split_tail.pdf";
            using (FileStream fileOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                outputStream.CopyTo(fileOut);
            }

            Console.WriteLine($"Split succeeded. Output saved to '{outputPath}'.");
        }
    }
}
