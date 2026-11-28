using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF file
        const string outputPath = "odd_multiple.pdf"; // result file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF into a memory stream
        using (FileStream inputFile = File.OpenRead(inputPath))
        using (MemoryStream sourceStream = new MemoryStream())
        {
            inputFile.CopyTo(sourceStream);
            sourceStream.Position = 0; // reset to beginning

            // Prepare the output stream where the extracted pages will be written
            using (FileStream outputFile = File.Create(outputPath))
            {
                // Aspose.Pdf uses 1‑based page numbers
                int[] pagesToExtract = new int[] { 13, 26, 39 };

                // PdfFileEditor does NOT implement IDisposable; instantiate directly
                PdfFileEditor editor = new PdfFileEditor();

                // Correct overload: Extract(Stream input, int[] pages, Stream output)
                editor.Extract(sourceStream, pagesToExtract, outputFile);
            }
        }

        Console.WriteLine($"Pages 13, 26, 39 have been extracted to '{outputPath}'.");
    }
}
