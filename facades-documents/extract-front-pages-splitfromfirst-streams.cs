using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string sourcePath = "input.pdf";

        // Desired end page (inclusive) for the extracted range starting from page 1
        const int endPage = 5;

        // Verify source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Prepare input and output streams
        using (FileStream inputStream = File.OpenRead(sourcePath))
        using (MemoryStream outputStream = new MemoryStream())
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
            PdfFileEditor editor = new PdfFileEditor();

            // Extract pages from the first page up to 'endPage' into the output stream
            // This overload works entirely in memory, no temporary files are created
            editor.SplitFromFirst(inputStream, endPage, outputStream);
        }

        // At this point 'outputStream' contains the extracted PDF.
        // To demonstrate usage, write the in‑memory PDF to a file.
        // (Re‑open the source and output streams to avoid disposing them above.)
        using (FileStream inputStream = File.OpenRead(sourcePath))
        using (MemoryStream tempOutput = new MemoryStream())
        {
            PdfFileEditor editor = new PdfFileEditor();
            editor.SplitFromFirst(inputStream, endPage, tempOutput);

            // Reset position before reading/writing
            tempOutput.Position = 0;

            const string resultPath = "extracted_pages.pdf";
            using (FileStream fileOut = File.Create(resultPath))
            {
                tempOutput.CopyTo(fileOut);
            }

            Console.WriteLine($"Pages 1‑{endPage} extracted to '{resultPath}'.");
        }
    }
}