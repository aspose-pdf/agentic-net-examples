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

        // Load the PDF document; the using block ensures it is disposed (closed) automatically.
        using (Document doc = new Document(inputPath))
        {
            // Document is now loaded. No further processing is required.
            Console.WriteLine($"Document loaded successfully. Page count: {doc.Pages.Count}");
        } // Document.Dispose() is called here.

        Console.WriteLine("Document has been closed.");
    }
}