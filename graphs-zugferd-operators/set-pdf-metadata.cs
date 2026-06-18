using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample PDF
        using (Document createDoc = new Document())
        {
            createDoc.Pages.Add();
            createDoc.Save("input.pdf");
        }

        // Open the PDF and set metadata
        using (Document doc = new Document("input.pdf"))
        {
            // Set predefined metadata
            doc.Info.Title = "Invoice 2023";
            doc.Info.Author = "Acme Corp";

            // Add custom property
            doc.Info.Add("InvoiceNumber", "INV-001");

            // Save the updated PDF
            doc.Save("output.pdf");
        }
    }
}