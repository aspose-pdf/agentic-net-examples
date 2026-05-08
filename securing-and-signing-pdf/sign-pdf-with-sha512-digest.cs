using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_output.pdf"; // signed PDF
        const string pfxPath    = "certificate.pfx";   // PKCS#12 file containing the signing certificate
        const string pfxPassword = "password";         // password for the PFX file

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a visible signature field on the first page
            // Rectangle constructor: (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                PartialName = "Signature1" // field identifier
            };
            doc.Form.Add(sigField);

            // Load the signing certificate (private key must be exportable or accessible)
            X509Certificate2 cert = new X509Certificate2(
                pfxPath,
                pfxPassword,
                X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);

            // Create an ExternalSignature that uses SHA‑512 as the digest algorithm
            ExternalSignature externalSig = new ExternalSignature(cert, DigestHashAlgorithm.Sha512)
            {
                Reason      = "Document approval",
                ContactInfo = "contact@example.com",
                Location    = "Head Office"
            };

            // Sign the field with the prepared signature object
            sigField.Sign(externalSig);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdf}'.");
    }
}