using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Set the global file size limit for loading entire files into memory to 2 MB.
        // This limit also applies to attached file fields when the form is processed.
        Document.FileSizeLimitToMemoryLoading = 2;

        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document, respecting the file size limit set above.
        using (Document doc = new Document(inputPath))
        {
            // No additional modifications are required for the limit to take effect.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with a 2 MB file size limit: {outputPath}");
    }
}