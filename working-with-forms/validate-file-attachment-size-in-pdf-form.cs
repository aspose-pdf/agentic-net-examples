using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Maximum allowed file size: 5 MB (in bytes)
    private const long MaxFileSizeBytes = 5L * 1024 * 1024;

    static void Main(string[] args)
    {
        // Expect a file path as the first argument
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: program <pdf-file-path>");
            return;
        }

        string filePath = args[0];

        // Ensure the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }

        // Validate the file size against the 5 MB limit
        if (!IsFileSizeValid(filePath))
        {
            Console.WriteLine("The selected file exceeds the 5 MB size limit.");
            return;
        }

        // At this point the file size is acceptable; proceed with further processing
        // Example: load the PDF with Aspose.Pdf core API and display page count
        Document pdfDocument = new Document(filePath);
        Console.WriteLine($"PDF loaded successfully. Page count: {pdfDocument.Pages.Count}");

        // Example: save a copy of the PDF (could be any further processing)
        string outputPath = Path.Combine(Path.GetDirectoryName(filePath) ?? string.Empty, "processed.pdf");
        pdfDocument.Save(outputPath);
        Console.WriteLine($"Processed file saved to: {outputPath}");
    }

    private static bool IsFileSizeValid(string path)
    {
        FileInfo info = new FileInfo(path);
        return info.Length <= MaxFileSizeBytes;
    }
}