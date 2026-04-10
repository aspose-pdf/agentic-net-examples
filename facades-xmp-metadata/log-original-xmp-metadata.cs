using System;
using System.IO;
using System.Text;
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
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle managed by using)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the XMP metadata facade and bind it to the document
            using (PdfXmpMetadata xmpFacade = new PdfXmpMetadata())
            {
                xmpFacade.BindPdf(pdfDoc);

                // Retrieve the XMP metadata as a byte array
                byte[] xmpBytes = xmpFacade.GetXmpMetadata();

                // Convert the byte array to a UTF‑8 string (XML format)
                string xmpXml = Encoding.UTF8.GetString(xmpBytes);

                // Log the original XMP XML to the console for audit purposes
                Console.WriteLine("Original XMP Metadata:");
                Console.WriteLine(xmpXml);

                // Persist the XMP XML to an audit file (ensuring the file is closed promptly)
                File.WriteAllText(auditLogPath, xmpXml, Encoding.UTF8);
            }
        }
    }
}
