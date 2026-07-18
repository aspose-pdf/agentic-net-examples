using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF metadata via PdfFileInfo facade
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);

        // Check if the Author field is empty or whitespace
        if (string.IsNullOrWhiteSpace(pdfInfo.Author))
        {
            // Set the Author only when it is missing
            pdfInfo.Author = "Default Author";
        }

        // Save the updated metadata to a new file
        bool saved = pdfInfo.SaveNewInfo(outputPath);
        Console.WriteLine(saved
            ? $"Metadata saved to '{outputPath}'."
            : $"Failed to save metadata to '{outputPath}'.");
    }
}