using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string auditLogPath = "xmp_audit.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document and extract its original XMP metadata
        using (Document pdfDoc = new Document(inputPdfPath))
        using (PdfXmpMetadata xmpFacade = new PdfXmpMetadata())
        {
            // Initialize the facade with the loaded document
            xmpFacade.BindPdf(pdfDoc);

            // Get the complete XMP packet as a byte array
            byte[] xmpBytes = xmpFacade.GetXmpMetadata();

            // Convert the byte array to a UTF‑8 string for logging
            string xmpXml = System.Text.Encoding.UTF8.GetString(xmpBytes);

            // Persist the original XMP XML to an audit file
            File.WriteAllText(auditLogPath, xmpXml);

            Console.WriteLine($"Original XMP metadata saved to '{auditLogPath}'.");
        }
    }
}