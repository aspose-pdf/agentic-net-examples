using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const int startPage = 5; // page from which to split to the end

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // Destination will be kept in memory
            using (MemoryStream outputStream = new MemoryStream())
            {
                PdfFileEditor editor = new PdfFileEditor();

                // Split from startPage to the end of the document
                bool result = editor.SplitToEnd(inputStream, startPage, outputStream);

                if (!result)
                {
                    Console.Error.WriteLine("Split operation failed.");
                    return;
                }

                // Reset the output stream position before reading from it
                outputStream.Position = 0;

                // Example: write the split part to a physical file
                const string outputPath = "split_part.pdf";
                using (FileStream fileOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    outputStream.CopyTo(fileOut);
                }

                Console.WriteLine($"Pages {startPage}‑{int.MaxValue} saved to '{outputPath}'.");
            }
        }
    }
}