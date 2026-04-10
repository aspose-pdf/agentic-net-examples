using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the thumbprint of the certificate to use.
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string thumbprint = "YOUR_CERTIFICATE_THUMBPRINT"; // without spaces, case‑insensitive

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the visible signature will appear.
            // Parameters: llx, lly, urx, ury (lower‑left X/Y, upper‑right X/Y).
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page.
            Page page = doc.Pages[1];
            SignatureField sigField = new SignatureField(page, sigRect);
            page.Annotations.Add(sigField); // attach the field to the page

            // Retrieve the certificate from the current user's Personal store by thumbprint.
            X509Certificate2 certificate = GetCertificateByThumbprint(thumbprint);
            if (certificate == null)
            {
                Console.Error.WriteLine("Certificate not found or does not contain a private key.");
                return;
            }

            // Create an external PKCS#7 signature using the certificate.
            ExternalSignature externalSig = new ExternalSignature(certificate);
            externalSig.Reason      = "Document approved";
            externalSig.ContactInfo = "contact@example.com";
            externalSig.Location    = "New York, USA";

            // Sign the field with the external signature.
            sigField.Sign(externalSig);

            // Save the signed PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }

    // Helper method to locate a certificate in the CurrentUser\My store by thumbprint.
    private static X509Certificate2 GetCertificateByThumbprint(string thumbprint)
    {
        if (string.IsNullOrWhiteSpace(thumbprint))
            return null;

        // Remove any whitespace from the thumbprint string.
        string cleanedThumbprint = thumbprint.Replace(" ", string.Empty).ToUpperInvariant();

        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection found = store.Certificates.Find(
                X509FindType.FindByThumbprint,
                cleanedThumbprint,
                validOnly: false);

            if (found.Count > 0 && found[0].HasPrivateKey)
                return found[0];
        }

        return null;
    }
}