using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF, modify its metadata, and save it.
        // Document is wrapped in a using block for deterministic disposal (lifecycle rule).
        using (Document doc = new Document(inputPath))
        {
            // Set standard document properties.
            doc.Info.Title = "Invoice 2023-07";
            doc.Info.Author = "Acme Corp";

            // Add a custom property to store the invoice number using the DocumentInfo indexer.
            doc.Info["InvoiceNumber"] = "INV-202307-001";

            // Save the updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated metadata to '{outputPath}'.");
    }
}
