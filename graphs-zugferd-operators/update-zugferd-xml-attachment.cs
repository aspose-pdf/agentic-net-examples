using System;
using System.IO;
using Aspose.Pdf;

class UpdateZugferd
{
    static void Main()
    {
        // Paths to the source PDF, the new ZUGFeRD XML, and the output PDF
        const string pdfPath      = "invoice.pdf";
        const string newXmlPath   = "new_invoice.xml";
        const string outputPdfPath = "invoice_updated.pdf";

        // Verify that the input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(newXmlPath))
        {
            Console.Error.WriteLine($"XML not found: {newXmlPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Replace the embedded ZUGFeRD XML attachment with the new XML.
                // BindXml replaces the existing XML (e.g., XFA or embedded XML) in the PDF.
                doc.BindXml(newXmlPath);

                // Save the updated PDF. No SaveOptions are required because we are saving as PDF.
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"ZUGFeRD XML updated and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error updating PDF: {ex.Message}");
        }
    }
}