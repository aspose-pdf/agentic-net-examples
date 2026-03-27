using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int startPage = 3; // split from this page to the end (1‑based index)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so instantiate it without a using block
        var pdfEditor = new PdfFileEditor();

        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream outputStream = new MemoryStream())
        {
            bool splitResult = pdfEditor.SplitToEnd(inputStream, startPage, outputStream);
            if (!splitResult)
            {
                Console.Error.WriteLine("Split operation failed.");
                return;
            }

            // Reset position before writing the result to a file
            outputStream.Position = 0;
            using (FileStream fileOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                outputStream.CopyTo(fileOut);
            }
        }

        Console.WriteLine($"Pages from {startPage} to end saved to '{outputPath}'.");
    }
}