using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input PDF containing a ZUGFeRD invoice
        const string inputPdfPath = "invoice.pdf";
        // XML file with updated ZUGFeRD data (buyer, seller, line items)
        const string zugferdXmlPath = "updated_invoice.xml";
        // Path for the PDF after the XML data has been applied
        const string outputPdfPath = "invoice_updated.pdf";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(zugferdXmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugferdXmlPath}");
            return;
        }

        // ------------------------------------------------------------
        // Load the original PDF, bind the ZUGFeRD XML, and save it.
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // BindXml replaces the embedded XML part of the PDF with the
            // supplied ZUGFeRD XML, thereby updating buyer, seller and line items.
            pdfDoc.BindXml(zugferdXmlPath);

            // Save the modified PDF. No SaveOptions are needed because the
            // target format is PDF.
            pdfDoc.Save(outputPdfPath);
        }

        // ------------------------------------------------------------
        // Reload the updated PDF to confirm that it can be opened again.
        // ------------------------------------------------------------
        using (Document reloadedDoc = new Document(outputPdfPath))
        {
            // Example verification: output the page count.
            Console.WriteLine($"Reloaded PDF page count: {reloadedDoc.Pages.Count}");
        }
    }
}