using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the certificate from a hardware token (PKCS#11). Replace the thumbprint with the actual value.
        string certThumbprint = "YOUR_CERT_THUMBPRINT";
        X509Certificate2 cert = GetCertificateFromStore(certThumbprint);
        if (cert == null)
        {
            Console.Error.WriteLine("Certificate not found on token or does not have a private key.");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Create a signature field on the first page.
            // Fully qualified rectangle to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField signatureField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(signatureField, 1);

            // Create an external signature using the certificate from the token.
            ExternalSignature externalSignature = new ExternalSignature(cert);

            // Sign the document.
            signatureField.Sign(externalSignature);

            // Save the signed PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }

    // Retrieves a certificate with the specified thumbprint from the CurrentUser store.
    // The certificate must have an accessible private key (e.g., on a hardware token).
    static X509Certificate2 GetCertificateFromStore(string thumbprint)
    {
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            foreach (X509Certificate2 cert in store.Certificates)
            {
                if (string.Equals(cert.Thumbprint, thumbprint, StringComparison.OrdinalIgnoreCase) && cert.HasPrivateKey)
                {
                    return cert;
                }
            }
        }
        return null;
    }
}