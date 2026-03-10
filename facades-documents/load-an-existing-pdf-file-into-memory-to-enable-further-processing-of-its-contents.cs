using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into memory using the Document constructor (preferred over PdfViewer for document manipulation)
        Document doc = new Document(inputPath);

        // Example operation: display the page count
        Console.WriteLine($"Loaded PDF has {doc.Pages.Count} pages.");

        // Additional processing can be performed on 'doc' here
    }
}
