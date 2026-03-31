using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF file information
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);

        // Retrieve the Author metadata
        string author = fileInfo.Author;

        Console.WriteLine($"Author: {author}");
    }
}
