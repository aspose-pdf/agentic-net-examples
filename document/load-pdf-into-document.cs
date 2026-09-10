using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the file exists before attempting to load it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF into a Document instance.
        // The using block ensures the Document is disposed properly,
        // releasing file handles and other resources.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // The document is now ready for further processing.
            Console.WriteLine($"PDF loaded successfully. Page count: {pdfDoc.Pages.Count}");
        }
    }
}