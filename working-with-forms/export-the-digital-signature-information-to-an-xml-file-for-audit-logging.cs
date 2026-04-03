using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed.pdf";
        const string xmlPath = "signature_audit.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Export the document (including signature fields) to XML for audit logging
            XmlSaveOptions xmlOptions = new XmlSaveOptions();
            pdfDoc.Save(xmlPath, xmlOptions);
        }

        Console.WriteLine($"Signature information exported to XML: {xmlPath}");
    }
}