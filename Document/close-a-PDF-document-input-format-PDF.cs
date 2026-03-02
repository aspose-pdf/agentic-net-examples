using System;
using System.IO;
using Aspose.Pdf;

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

        // Open the PDF document; the using block ensures Dispose() is called, closing the file.
        using (Document doc = new Document(inputPath))
        {
            // No processing required.
        }

        Console.WriteLine("PDF document closed successfully.");
    }
}