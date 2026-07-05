using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string title       = "Invoice #2023-001";
        const string author      = "Acme Corp";
        const string invoiceNum  = "2023-001";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, modify metadata, and save
        using (Document doc = new Document(inputPath))
        {
            // Set standard metadata fields
            doc.Info.Title  = title;
            doc.Info.Author = author;

            // Add a custom property (invoice number)
            doc.Info.Add("InvoiceNumber", invoiceNum);
            // Equivalent alternative:
            // doc.Info["InvoiceNumber"] = invoiceNum;

            // Persist changes
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated metadata to '{outputPath}'.");
    }
}