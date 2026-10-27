using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string invoiceNumber = "INV-20231001";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, modify its metadata, and save it.
        // Document is wrapped in a using block to ensure proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Set standard document properties.
            doc.Info.Title = "Invoice Document";
            doc.Info.Author = "Acme Corp";

            // Add a custom property for the invoice number using the indexer.
            doc.Info["InvoiceNumber"] = invoiceNumber;

            // Save the updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}
