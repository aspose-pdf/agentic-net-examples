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
        const string outputPdf = "signed.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document.
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdf))
        {
            // Ensure the document has at least one page.
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Define the rectangle where the visible signature will appear.
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the first page.
            Aspose.Pdf.Forms.SignatureField sigField = new Aspose.Pdf.Forms.SignatureField(doc, sigRect);
            doc.Pages[1].Annotations.Add(sigField);

            // Obtain the X509Certificate2 from the smart card (no PIN prompt).
            // This method should select a certificate whose private key is accessible
            // without UI interaction (e.g., using a CSP that does not request a PIN).
            X509Certificate2 cert = GetCertificateFromSmartCard();

            if (cert == null)
            {
                Console.Error.WriteLine("Smart card certificate not found.");
                return;
            }

            // Create an ExternalSignature that uses the smart‑card certificate.
            Aspose.Pdf.Forms.ExternalSignature externalSig = new Aspose.Pdf.Forms.ExternalSignature(cert)
            {
                Reason      = "Document approved",
                ContactInfo = "john.doe@example.com",
                Location    = "Head Office"
            };

            // Sign the field with the external signature.
            sigField.Sign(externalSig);

            // Save the signed PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }

    // Retrieves a certificate from the smart card. Adjust the search criteria as needed.
    private static X509Certificate2 GetCertificateFromSmartCard()
    {
        // Look in the CurrentUser's "My" store for a certificate that has a private key
        // and is marked as a smart‑card certificate (e.g., by checking the ProviderName).
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            foreach (X509Certificate2 cert in store.Certificates)
            {
                // Ensure the certificate has a private key.
                if (!cert.HasPrivateKey)
                    continue;

                // Example heuristic: the certificate's private key is provided by a CSP
                // whose name contains "SmartCard". Adjust according to your environment.
                var keyInfo = cert.PrivateKey?.ToString() ?? string.Empty;
                if (keyInfo.IndexOf("SmartCard", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return cert;
                }
            }
        }

        return null; // No suitable certificate found.
    }
}