using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string sourcePdfPath = "input.pdf";

        // Desired end page (inclusive) for the split operation
        // Aspose.Pdf uses 1‑based page indexing, so page numbers start at 1
        int endPage = 5;

        // Validate input file existence
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Perform the split using in‑memory streams
        using (FileStream inputStream = new FileStream(sourcePdfPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream outputStream = new MemoryStream())
        {
            // PdfFileEditor provides the SplitFromFirst method that works with streams
            PdfFileEditor editor = new PdfFileEditor();

            bool success = editor.SplitFromFirst(inputStream, endPage, outputStream);

            if (!success)
            {
                Console.Error.WriteLine("Split operation failed.");
                return;
            }

            // Reset the output stream position to the beginning for further processing or saving
            outputStream.Position = 0;

            // Example: write the resulting split PDF to a file (optional)
            const string splitPdfPath = "split_output.pdf";
            using (FileStream fileOut = new FileStream(splitPdfPath, FileMode.Create, FileAccess.Write))
            {
                outputStream.CopyTo(fileOut);
            }

            Console.WriteLine($"PDF split successfully. Front part saved to '{splitPdfPath}'.");
        }
    }
}