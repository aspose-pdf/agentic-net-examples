using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        // Ensure the source file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF file as a stream and load it into a Document object
        using (FileStream pdfStream = File.OpenRead(pdfPath))
        {
            // Aspose.Pdf.Document can be instantiated directly from a stream
            Document doc = new Document(pdfStream);

            // Example usage: output the number of pages
            Console.WriteLine($"Loaded PDF has {doc.Pages.Count} page(s).");
        }
    }
}