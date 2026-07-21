using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string pfxPath   = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the certificate (must contain a private key)
        X509Certificate2 cert = new X509Certificate2(pfxPath, pfxPassword, X509KeyStorageFlags.Exportable);

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a signature field on the first page
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                PartialName = "Signature1"
            };

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField, 1);

            // Create an ExternalSignature using SHA‑512 digest algorithm
            ExternalSignature externalSig = new ExternalSignature(cert, DigestHashAlgorithm.Sha512)
            {
                Reason      = "Document approved",
                ContactInfo = "contact@example.com",
                Location    = "New York"
            };

            // Sign the field with the prepared signature object
            sigField.Sign(externalSig);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}