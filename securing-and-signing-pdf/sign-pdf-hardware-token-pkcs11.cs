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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (1‑based page indexing)
        using (Document doc = new Document(inputPdf))
        {
            // Create a signature field on the first page
            Page page = doc.Pages[1];
            // Define the rectangle where the signature will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            // Add the signature field annotation
            SignatureField signatureField = new SignatureField(page, rect);
            page.Annotations.Add(signatureField);

            // Obtain the signing certificate from a hardware token (PKCS#11)
            // The certificate is accessed via the Windows certificate store;
            // the private key remains non‑exportable and is used by the token.
            X509Certificate2 signingCert = null;
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);
                // Adjust the selection logic as needed (e.g., by subject name)
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

            // Create an ExternalSignature that uses the hardware token's private key
            ExternalSignature externalSignature = new ExternalSignature(signingCert);

            // Optional: set signature appearance properties
            externalSignature.Reason = "Document approval";
            externalSignature.Location = "Office";
            externalSignature.ContactInfo = "user@example.com";
            externalSignature.Date = DateTime.UtcNow;

            // Sign the PDF using the signature field
            signatureField.Sign(externalSignature);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}