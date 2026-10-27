using System;
using System.IO;
using Aspose.Pdf; // Document, DocumentInfo reside in this namespace

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document. The Document class implements IDisposable, so we wrap it in a using block.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // DocumentInfo can be created from a Document instance (not directly from a file path).
            DocumentInfo fileInfo = new DocumentInfo(pdfDocument);

            // The PDF version is exposed by the Document.Version property (not by DocumentInfo).
            string pdfVersion = pdfDocument.Version;

            Console.WriteLine($"PDF version: {pdfVersion}");
            // The pdfVersion variable can now be used elsewhere in the program.
        }
    }
}
