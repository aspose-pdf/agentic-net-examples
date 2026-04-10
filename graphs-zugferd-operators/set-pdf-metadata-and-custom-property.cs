using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Set standard metadata fields
            doc.Info.Title  = "Invoice 2023-001";
            doc.Info.Author = "Acme Corp";

            // Add a custom metadata entry for the invoice number
            doc.Info.Add("InvoiceNumber", "2023-001");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with metadata to '{outputPath}'.");
    }
}