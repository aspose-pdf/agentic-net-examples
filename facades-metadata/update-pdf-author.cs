using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newAuthor = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file information
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        // Set the new Author value
        fileInfo.Author = newAuthor;
        // Persist the changes to a new PDF file
        bool success = fileInfo.SaveNewInfo(outputPath);
        Console.WriteLine(success ? $"Author updated and saved to '{outputPath}'." : "Failed to save updated PDF.");
    }
}
