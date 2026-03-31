using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Retrieve PDF version using PdfFileInfo
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        string pdfVersion = fileInfo.GetPdfVersion();

        // Also demonstrate Document.Version property (requires proper disposal)
        using (Document doc = new Document(inputPath))
        {
            string docVersion = doc.Version;
            Console.WriteLine($"PdfFileInfo version: {pdfVersion}");
            Console.WriteLine($"Document.Version property: {docVersion}");
        }

        // Store the version for later use
        string storedVersion = pdfVersion;
        // Example usage of the stored version
        Console.WriteLine($"Stored version for later use: {storedVersion}");
    }
}