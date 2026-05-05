using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (2‑up layout after deletions)
        const string outputPath = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Pages to delete (1‑based indexing). Adjust as needed.
        int[] pagesToDelete = new int[] { 2, 3 };

        // Facade for PDF file operations
        PdfFileEditor pdfEditor = new PdfFileEditor();

        // First step: delete specified pages and store the result in a memory stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream intermediateStream = new MemoryStream())
        {
            bool deleteSuccess = pdfEditor.Delete(inputStream, pagesToDelete, intermediateStream);
            if (!deleteSuccess)
            {
                Console.Error.WriteLine("Failed to delete pages.");
                return;
            }

            // Reset the position of the intermediate stream before the next operation
            intermediateStream.Position = 0;

            // Second step: create a 2‑up layout (2 columns, 1 row) and write to the final output stream
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                bool nupSuccess = pdfEditor.MakeNUp(intermediateStream, outputStream, 2, 1);
                if (!nupSuccess)
                {
                    Console.Error.WriteLine("Failed to create 2‑up layout.");
                    return;
                }
            }
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}