using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF file information using the Facades API (optional, kept for other info)
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);

        // Retrieve the PDF version using the Document class, which exposes the Version property
        string pdfVersion;
        using (Document doc = new Document(inputPath))
        {
            pdfVersion = doc.Version; // e.g., "1.7"
        }

        // Example usage of the stored version
        Console.WriteLine($"PDF version: {pdfVersion}");
    }
}