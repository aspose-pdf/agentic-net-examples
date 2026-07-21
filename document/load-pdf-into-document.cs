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

        // Load the PDF file into a Document instance.
        // The using block ensures the Document is disposed properly.
        using (Document doc = new Document(inputPath))
        {
            // The document is now ready for further processing.
            Console.WriteLine($"PDF loaded successfully. Page count: {doc.Pages.Count}");
        }
    }
}