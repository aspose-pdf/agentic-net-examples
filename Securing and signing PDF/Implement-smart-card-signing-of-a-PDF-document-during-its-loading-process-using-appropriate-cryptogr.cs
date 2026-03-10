using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class SmartCardPdfSigner
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Define a rectangle where the signature appearance will be placed
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Add a signature field to the first page
            Page firstPage = pdfDoc.Pages[1];
            SignatureField sigField = new SignatureField(firstPage, sigRect);
            // Set the field name (PartialName) and add it to the form. The Add overload expects a page number (int).
            sigField.PartialName = "Signature1";
            pdfDoc.Form.Add(sigField, 1);

            // Retrieve the signing certificate from a smart‑card / token
            X509Certificate2 signingCert = GetSmartCardCertificate();
            if (signingCert == null)
            {
                Console.Error.WriteLine("No suitable signing certificate found on the smart card.");
                return;
            }

            // Create an ExternalSignature that works with non‑exportable private keys
            ExternalSignature externalSig = new ExternalSignature(signingCert);

            // Optional: set signature properties
            externalSig.Reason = "Document approved";
            externalSig.Location = "Head Office";
            externalSig.Date = DateTime.Now;

            // Sign the document using the signature field (lifecycle rule: use Sign method)
            sigField.Sign(externalSig);

            // Save the signed PDF (lifecycle rule: use Document.Save)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }

    // Helper method to locate a certificate on a smart card / token.
    // Adjust the selection logic (e.g., Subject, Thumbprint) as needed.
    private static X509Certificate2 GetSmartCardCertificate()
    {
        // Open the personal (My) store of the current user.
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);

            // Find certificates that have a private key. For smart‑card keys we simply ensure the private key is present;
            // more specific hardware checks can be added if required.
            var cert = store.Certificates
                .OfType<X509Certificate2>()
                .FirstOrDefault(c => c.HasPrivateKey && c.GetRSAPrivateKey() != null);

            // If no certificate with an RSA private key is found, fall back to any certificate that has a private key.
            if (cert == null)
            {
                cert = store.Certificates
                    .OfType<X509Certificate2>()
                    .FirstOrDefault(c => c.HasPrivateKey);
            }

            return cert;
        }
    }
}
