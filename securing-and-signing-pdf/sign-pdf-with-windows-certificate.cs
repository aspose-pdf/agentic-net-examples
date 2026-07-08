using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string thumbprint    = "YOUR_CERT_THUMBPRINT"; // replace with actual thumbprint (no spaces)

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Locate the certificate in the CurrentUser\My store by thumbprint
        X509Certificate2 cert = FindCertificateByThumbprint(thumbprint);
        if (cert == null)
        {
            Console.Error.WriteLine($"Certificate with thumbprint '{thumbprint}' not found in the store.");
            return;
        }

        // Create an ExternalSignature that uses the found certificate
        ExternalSignature externalSig = new ExternalSignature(cert);
        externalSig.Reason      = "Document approved";
        externalSig.ContactInfo = "contact@example.com";
        externalSig.Location    = "New York, USA";

        // Open the PDF, add a signature field (if needed), and sign it
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Define the rectangle where the visible signature will appear
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(pdfDoc.Pages[1], sigRect);
            sigField.PartialName = "Signature1";

            // Add the signature field to the document's form
            pdfDoc.Form.Add(sigField, 1);

            // Sign the field using the external certificate
            sigField.Sign(externalSig);

            // Save the signed PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }

    // Helper method to locate a certificate by thumbprint in the CurrentUser\My store
    private static X509Certificate2 FindCertificateByThumbprint(string thumbprint)
    {
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            foreach (X509Certificate2 cert in store.Certificates)
            {
                // Thumbprint may contain spaces; remove them and compare case‑insensitively
                string cleanedThumb = cert.Thumbprint?.Replace(" ", string.Empty);
                if (string.Equals(cleanedThumb, thumbprint.Replace(" ", string.Empty), StringComparison.OrdinalIgnoreCase))
                {
                    return cert;
                }
            }
        }
        return null;
    }
}