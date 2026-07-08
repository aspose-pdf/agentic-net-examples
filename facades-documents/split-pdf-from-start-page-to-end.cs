using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF file
        const string outputPath = "split_end.pdf"; // rear part after split
        const int  startPage   = 5;               // split from this page (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Open streams for input and output. Streams are disposed automatically.
        using (FileStream inputStream  = new FileStream(inputPath,  FileMode.Open,  FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does NOT implement IDisposable; instantiate directly.
            PdfFileEditor editor = new PdfFileEditor();

            // Split from the specified start page to the end of the document.
            // The method returns true on success; handle the result if needed.
            bool success = editor.SplitToEnd(inputStream, startPage, outputStream);

            if (success)
                Console.WriteLine($"Successfully split PDF. Rear part saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Split operation failed.");
        }
    }
}