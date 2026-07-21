using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
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

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the signature will appear
            Rectangle sigRect = new Rectangle(100, 500, 300, 550);

            // Create a signature field on the page (use the (Page, Rectangle) ctor)
            SignatureField signatureField = new SignatureField(page, sigRect);
            signatureField.PartialName = "Signature1"; // set field name
            doc.Form.Add(signatureField);

            // Obtain the signing certificate from a hardware token (e.g., smart card)
            X509Certificate2 signingCert = GetCertificateFromStore();

            if (signingCert == null)
            {
                Console.Error.WriteLine("Signing certificate not found in the store.");
                return;
            }

            // Create an ExternalSignature that uses the certificate from the token
            ExternalSignature externalSignature = new ExternalSignature(signingCert)
            {
                Reason = "Document approved",
                Location = "Office",
                ContactInfo = "user@example.com"
            };

            // Sign the document using the signature field and the external signature
            signatureField.Sign(externalSignature);

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }

    // Helper method to locate a certificate with a private key in the current user's "My" store.
    // Adjust the search criteria (e.g., subject name) as needed for the specific token.
    static X509Certificate2 GetCertificateFromStore()
    {
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            foreach (X509Certificate2 cert in store.Certificates)
            {
                // Ensure the certificate has a private key and is usable for signing
                if (cert.HasPrivateKey)
                {
                    // Example filter: match by subject name (replace with actual criteria)
                    if (cert.Subject.Contains("Your Certificate Subject"))
                    {
                        return cert;
                    }
                }
            }
        }

        return null; // No suitable certificate found
    }
}
