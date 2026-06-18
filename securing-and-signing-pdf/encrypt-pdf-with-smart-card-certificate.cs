using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the encrypted output PDF
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "encrypted_output.pdf";

        // Identifier of the certificate stored on the smart card.
        // Adjust the search criteria (thumbprint, subject name, etc.) as needed.
        const string certificateThumbprint = "YOUR_CERTIFICATE_THUMBPRINT";

        // Locate the certificate on the smart card (CurrentUser\My store)
        X509Certificate2? cert = FindCertificateByThumbprint(certificateThumbprint);
        if (cert == null)
        {
            Console.Error.WriteLine("Certificate not found on the smart card.");
            return;
        }

        // Prepare the list of public certificates used for encryption.
        IList<X509Certificate2> publicCertificates = new List<X509Certificate2> { cert };

        // Open the PDF, encrypt it with the certificate, and save the result.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Define the permissions you want to allow for the encrypted document.
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt using a strong symmetric algorithm (AES‑256) and the public certificate.
            pdfDoc.Encrypt(permissions, CryptoAlgorithm.AESx256, publicCertificates);

            // Save the encrypted PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF encrypted successfully and saved to '{outputPdfPath}'.");
    }

    // Helper method to locate a certificate on the smart card by thumbprint.
    private static X509Certificate2? FindCertificateByThumbprint(string thumbprint)
    {
        // Open the personal certificate store of the current user.
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            foreach (X509Certificate2 cert in store.Certificates)
            {
                // Ensure the certificate has a private key (smart‑card based) and matches the thumbprint.
                if (cert.HasPrivateKey && string.Equals(cert.Thumbprint, thumbprint, StringComparison.OrdinalIgnoreCase))
                {
                    return cert;
                }
            }
        }
        return null;
    }
}