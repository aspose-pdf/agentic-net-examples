using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to source XML, certificate and output PDF
        const string xmlPath = "input.xml";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";
        const string outputPdf = "signed_output.pdf";

        // Verify required files exist
        if (!File.Exists(xmlPath) || !File.Exists(certPath))
        {
            Console.Error.WriteLine("XML or certificate file not found.");
            return;
        }

        // Load XML and convert it to a PDF document
        XmlLoadOptions xmlLoadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, xmlLoadOptions))
        {
            // Define the rectangle where the signature field will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(pdfDoc.Pages[1], rect);
            pdfDoc.Form.Add(sigField);

            // Use a concrete signature class (PKCS7) instead of the abstract Signature type
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword);
            pkcs7.Reason = "Document approval";
            pkcs7.Location = "Company HQ";
            // Optional: pkcs7.ContactInfo = "contact@example.com";

            // Apply the digital signature to the field
            sigField.Sign(pkcs7);

            // Save the signed PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
