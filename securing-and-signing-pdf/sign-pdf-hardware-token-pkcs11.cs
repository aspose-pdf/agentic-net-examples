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
        const string outputPdf = "signed_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // Obtain the signing certificate from a hardware token (PKCS#11).
            // This example assumes the certificate is stored in the current
            // user's "My" store and is accessible via the token.
            // Adjust the store name/location and selection logic as needed.
            // -----------------------------------------------------------------
            X509Certificate2 signingCert = null;
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);
                // TODO: Replace the selection logic with the appropriate certificate.
                // For demonstration, pick the first certificate that has a private key.
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
                Console.Error.WriteLine("No suitable signing certificate found in the token.");
                return;
            }

            // Create an ExternalSignature that works with certificates from tokens.
            ExternalSignature externalSignature = new ExternalSignature(signingCert)
            {
                Reason = "Document approved",
                Location = "Office",
                ContactInfo = "user@example.com"
            };

            // -----------------------------------------------------------------
            // Create a signature field on the first page.
            // -----------------------------------------------------------------
            Page firstPage = doc.Pages[1];
            // Define the rectangle where the signature will appear.
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            // Create the signature field annotation.
            SignatureField signatureField = new SignatureField(firstPage, sigRect)
            {
                PartialName = "Signature1"
                // Visual appearance can be customized via externalSignature.CustomAppearance if needed.
            };
            // Add the signature field to the page's annotations collection.
            firstPage.Annotations.Add(signatureField);

            // -----------------------------------------------------------------
            // Sign the document using the external signature (hardware token).
            // -----------------------------------------------------------------
            signatureField.Sign(externalSignature);

            // Save the signed PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
