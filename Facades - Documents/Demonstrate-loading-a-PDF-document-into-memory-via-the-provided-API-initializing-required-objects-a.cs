using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document directly using Aspose.Pdf.Document.
        // The Document constructor accepts a file path, a stream, or a byte array.
        Document doc = new Document(pdfPath);

        // Example operation: display the number of pages.
        Console.WriteLine($"Loaded PDF has {doc.Pages.Count} page(s).");
    }
}
