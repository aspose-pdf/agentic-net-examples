using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;               // for SignatureField, ExternalSignature, etc.
using Aspose.Pdf.Drawing;            // for Rectangle and other drawing types

class SignPdfWithHardwareToken
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

        // Load the PDF document (lifecycle: load)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure there is at least one page
            if (pdfDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Create a signature field on the first page
            Page firstPage = pdfDoc.Pages[1];

            // Define the rectangle where the signature will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the signature field annotation
            SignatureField signatureField = new SignatureField(firstPage, sigRect)
            {
                PartialName = "Signature1",
                // Optional visual properties
                Color = Aspose.Pdf.Color.LightGray
            };

            // Add the signature field to the page annotations collection
            firstPage.Annotations.Add(signatureField);

            // ------------------------------------------------------------
            // Obtain the signing certificate from a hardware token (PKCS#11)
            // The certificate is accessed via the Windows certificate store.
            // Adjust the store name/location and selection logic as needed.
            // ------------------------------------------------------------
            X509Certificate2? signingCert = null;
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);
                // Example: pick the first certificate that has a private key.
                // In production, locate the certificate by thumbprint or subject.
                foreach (var cert in store.Certificates)
                {
                    if (cert.HasPrivateKey)
                    {
                        signingCert = cert;
                        break;
                    }
                }
                store.Close();
            }

            if (signingCert == null)
            {
                Console.Error.WriteLine("No suitable certificate with a private key was found in the store.");
                return;
            }

            // Create an ExternalSignature that uses the certificate from the token.
            // ExternalSignature supports non‑exportable private keys (e.g., smart cards, HSMs).
            ExternalSignature externalSignature = new ExternalSignature(signingCert);

            // Optional: set signature appearance properties (property names are case‑sensitive)
            externalSignature.Reason = "Document approval";
            externalSignature.Location = "Head Office";
            externalSignature.ContactInfo = "contact@example.com";
            // The SignDate property is not available on ExternalSignature in recent versions;
            // the signing date is set automatically, so we omit it.

            // Sign the PDF using the signature field and the external signature.
            signatureField.Sign(externalSignature);

            // Save the signed PDF (lifecycle: save)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}
