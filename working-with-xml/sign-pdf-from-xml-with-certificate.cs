using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for input XML, intermediate PDF, signed PDF and certificate
        const string xmlPath = "input.xml";
        const string pdfPath = "intermediate.pdf";
        const string signedPdfPath = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "certPassword";

        // Validate required files
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Convert XML to PDF using XmlLoadOptions
        var xmlLoadOptions = new XmlLoadOptions();
        using (var pdfDoc = new Document(xmlPath, xmlLoadOptions))
        {
            pdfDoc.Save(pdfPath);
        }

        // Load the generated PDF for signing
        var doc = new Document(pdfPath);

        // Define the signature rectangle (left, bottom, right, top) in points
        var rect = new Rectangle(100, 100, 300, 160);

        // Create a signature field on the first page
        var signatureField = new SignatureField(doc.Pages[1], rect)
        {
            PartialName = "Signature1"
        };
        // Add the field to the document's form collection (position 1 = first field)
        doc.Form.Add(signatureField, 1);

        // Prepare the PKCS7 signature object
        var pkcs7 = new PKCS7(certPath, certPassword)
        {
            Reason = "Document signed for authenticity",
            Location = "Location",
            ContactInfo = "contact@example.com"
        };

        // Sign the field – this writes the digital signature into the PDF
        signatureField.Sign(pkcs7);

        // Save the signed PDF
        doc.Save(signedPdfPath);

        Console.WriteLine($"Signed PDF saved to '{signedPdfPath}'.");
    }
}
