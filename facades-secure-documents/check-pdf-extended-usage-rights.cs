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

        // Load the PDF document (required for proper resource handling)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfFileSignature facade and bind the PDF file
            using (PdfFileSignature signature = new PdfFileSignature())
            {
                signature.BindPdf(inputPath); // alternatively: signature.BindPdf(doc);

                // Check for extended usage rights
                bool hasUsageRights = signature.ContainsUsageRights();

                Console.WriteLine($"Contains usage rights: {hasUsageRights}");
            }
        }
    }
}