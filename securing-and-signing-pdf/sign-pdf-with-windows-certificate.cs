using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF to be signed
        const string inputPdfPath = "input.pdf";
        // Output signed PDF
        const string outputPdfPath = "signed_output.pdf";
        // Thumbprint of the certificate in the Windows certificate store (no spaces, case‑insensitive)
        const string certThumbprint = "YOUR_CERTIFICATE_THUMBPRINT";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the certificate from the CurrentUser\My store
        X509Certificate2 certificate = GetCertificateByThumbprint(certThumbprint);
        if (certificate == null)
        {
            Console.Error.WriteLine($"Certificate with thumbprint '{certThumbprint}' not found.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a signature field on the first page (adjust rectangle as needed)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect);
            // Add the signature field to the page annotations collection
            doc.Pages[1].Annotations.Add(sigField);

            // Create an ExternalSignature that uses the X509Certificate2 from the store
            ExternalSignature externalSig = new ExternalSignature(certificate);
            // Optional: set appearance properties
            externalSig.Reason = "Document approved";
            externalSig.Location = "Office";
            externalSig.ContactInfo = "contact@example.com";

            // Sign the document using the signature field
            sigField.Sign(externalSig);

            // Save the signed PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }

    // Helper method to retrieve a certificate by thumbprint from the CurrentUser\My store
    private static X509Certificate2 GetCertificateByThumbprint(string thumbprint)
    {
        // Normalize thumbprint (remove spaces, make uppercase)
        string normalizedThumbprint = thumbprint.Replace(" ", string.Empty).ToUpperInvariant();

        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection found = store.Certificates.Find(
                X509FindType.FindByThumbprint,
                normalizedThumbprint,
                validOnly: false);

            return found.Count > 0 ? found[0] : null;
        }
    }
}