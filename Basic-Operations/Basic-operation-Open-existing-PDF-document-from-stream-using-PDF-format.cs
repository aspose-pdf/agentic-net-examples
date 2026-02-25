using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_copy.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF file as a stream and load it into an Aspose.Pdf Document.
        // Both the FileStream and Document are wrapped in using blocks for deterministic disposal.
        using (FileStream pdfStream = File.OpenRead(inputPath))
        using (Document doc = new Document(pdfStream))
        {
            // Example operation: display the number of pages.
            Console.WriteLine($"Pages in document: {doc.Pages.Count}");

            // Save the document to a new file (basic copy operation).
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document opened from stream and saved to '{outputPath}'.");
    }
}