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

        // Load the PDF file into a Document object (lifecycle rule: use constructor)
        using (Document doc = new Document(inputPath))
        {
            // Example operation: display the number of pages
            Console.WriteLine($"PDF loaded successfully. Page count: {doc.Pages.Count}");

            // Additional processing can be performed here
        }
    }
}