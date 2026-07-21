using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string auditXmlPath = "original_xmp.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor inside using)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create the XMP metadata facade (lifecycle rule: use PdfXmpMetadata constructor)
            using (PdfXmpMetadata xmpFacade = new PdfXmpMetadata())
            {
                // Bind the PDF to the facade (rule: BindPdf(Document))
                xmpFacade.BindPdf(pdfDoc);

                // Retrieve the XMP metadata as a byte array (rule: GetXmpMetadata())
                byte[] xmpBytes = xmpFacade.GetXmpMetadata();

                // Convert the byte array to a UTF‑8 string (XMP is XML)
                string xmpXml = System.Text.Encoding.UTF8.GetString(xmpBytes);

                // Write the original XMP XML to an audit file
                File.WriteAllText(auditXmlPath, xmpXml);
                Console.WriteLine($"Original XMP metadata saved to '{auditXmlPath}'.");
            }

            // -----------------------------------------------------------------
            // Place any further PDF modifications here.
            // -----------------------------------------------------------------

            // Example: save the (potentially) modified PDF (optional)
            // pdfDoc.Save("modified.pdf");
        }
    }
}