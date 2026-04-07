using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Set the global file size limit for loading files into memory to 2 MB.
        // This limit applies to file‑attachment fields and other file‑loading operations.
        Document.FileSizeLimitToMemoryLoading = 2;

        // Load the PDF document (using the standard load rule)
        using (Document doc = new Document(inputPath))
        {
            // Save the PDF document (using the standard save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with a 2 MB file‑size limit: {outputPath}");
    }
}