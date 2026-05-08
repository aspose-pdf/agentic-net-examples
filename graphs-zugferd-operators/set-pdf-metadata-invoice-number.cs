using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "invoice.pdf";

        // Create a new PDF document (empty)
        using (Document doc = new Document())
        {
            // Set standard metadata properties
            doc.Info.Title = "Invoice #12345";
            doc.Info.Author = "Acme Corp";

            // Add a custom metadata entry for the invoice number
            doc.Info.Add("InvoiceNumber", "12345");

            // Ensure the document has at least one page
            doc.Pages.Add();

            // Save the PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}