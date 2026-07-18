using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Generate a new GUID to use as the document identifier
        string documentId = Guid.NewGuid().ToString();

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfFileInfo facade on the loaded document
            using (PdfFileInfo fileInfo = new PdfFileInfo(doc))
            {
                // Set a custom header entry with the generated GUID
                fileInfo.Header = new Dictionary<string, string>
                {
                    { "DocumentID", documentId }
                };

                // Save the updated PDF with the new header information
                fileInfo.SaveNewInfo(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with Document ID: {documentId}");
    }
}