using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";
        const string signedPdfPath = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "certPass";

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

        // Convert XML to PDF
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            doc.Save(pdfPath);
        }

        // Load the generated PDF for signing
        using (Document signedDoc = new Document(pdfPath))
        {
            // Define the rectangle where the visible signature will appear (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            var signatureRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page (page index 1)
            var signatureField = new SignatureField(signedDoc, signatureRect)
            {
                Name = "Signature1"
            };
            signedDoc.Form.Add(signatureField);

            // Load the PKCS#7 certificate
            var pkcs7 = new PKCS7(certPath, certPassword);

            // Apply the digital signature
            signatureField.Sign(pkcs7);

            // Save the signed PDF
            signedDoc.Save(signedPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{signedPdfPath}'.");
    }
}
