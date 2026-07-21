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

        try
        {
            // Load the PDF document (lifecycle: create & load)
            using (Document doc = new Document(inputPath))
            {
                // Initialize the PdfFileSignature facade
                using (PdfFileSignature signature = new PdfFileSignature())
                {
                    // Bind the loaded document to the facade
                    signature.BindPdf(doc);

                    // Detect extended usage rights
                    bool hasUsageRights = signature.ContainsUsageRights();

                    Console.WriteLine($"Contains usage rights: {hasUsageRights}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}