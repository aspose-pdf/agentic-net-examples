using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string invoiceNumber = "INV-001";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Set standard metadata properties
            doc.Info.Title  = "Invoice Document";
            doc.Info.Author = "Acme Corp";

            // Add a custom property (key/value) to the document info dictionary
            doc.Info.Add("InvoiceNumber", invoiceNumber);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with title, author, and invoice number to '{outputPath}'.");
    }
}