using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string invoiceNumber = "INV-20230818";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Set standard metadata properties
            doc.Info.Title = "Invoice Document";
            doc.Info.Author = "Acme Corp";

            // Add a custom metadata entry for the invoice number
            doc.Info.Add("InvoiceNumber", invoiceNumber);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with metadata to '{outputPath}'.");
    }
}