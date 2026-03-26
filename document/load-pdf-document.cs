using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file using the Document constructor (or instantiate DocumentFactory first if needed)
        using (Document document = new Document(inputPath))
        {
            // Example operation: display the number of pages
            Console.WriteLine($"Loaded PDF with {document.Pages.Count} pages.");

            // Optional: save to a new file to complete the lifecycle
            document.Save(outputPath);
        }

        Console.WriteLine($"Document saved as '{outputPath}'.");
    }
}