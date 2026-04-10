using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the signature facade
            using (PdfFileSignature signature = new PdfFileSignature())
            {
                // Bind the document to the facade
                signature.BindPdf(doc);

                // Detect extended usage rights
                bool hasUsageRights = signature.ContainsUsageRights();

                Console.WriteLine($"Extended usage rights present: {hasUsageRights}");
            }
        }
    }
}