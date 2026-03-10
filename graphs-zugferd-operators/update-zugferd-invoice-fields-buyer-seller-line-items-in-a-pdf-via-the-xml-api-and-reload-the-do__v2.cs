using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, ZUGFeRD XML data and the intermediate output PDF
        const string sourcePdfPath = "invoice.pdf";
        const string zugferdXmlPath = "invoice.xml";
        const string updatedPdfPath = "invoice_updated.pdf";

        // Verify that the required files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(zugferdXmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugferdXmlPath}");
            return;
        }

        // Load the original PDF, bind the ZUGFeRD XML, and save the updated document
        using (Document pdfDoc = new Document(sourcePdfPath))
        {
            // Bind the ZUGFeRD XML data to the PDF using the XML API
            using (FileStream xmlStream = File.OpenRead(zugferdXmlPath))
            {
                pdfDoc.BindXml(xmlStream);
            }

            // Save the PDF with the embedded ZUGFeRD data
            pdfDoc.Save(updatedPdfPath);
        }

        // Reload the updated PDF to verify that the XML was embedded correctly
        using (Document reloadedDoc = new Document(updatedPdfPath))
        {
            // Example: check that the document now contains XML data
            // The BindXml method can also be used to retrieve the XML; here we just confirm the document loads without error
            Console.WriteLine($"Reloaded PDF page count: {reloadedDoc.Pages.Count}");
        }

        Console.WriteLine($"ZUGFeRD data embedded and PDF saved to '{updatedPdfPath}'.");
    }
}