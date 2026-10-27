using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string thumbprint = "YOUR_CERT_THUMBPRINT"; // replace with actual thumbprint
        const string pfxPath = "certificate.pfx"; // path to a PFX that references the smart‑card key (or a placeholder for compilation)
        const string pfxPassword = ""; // empty if the private key is on a smart‑card and does not require a password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the certificate (with private key) from the smart‑card (CurrentUser\My store)
        X509Certificate2 cert = null;
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            var certs = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
            if (certs.Count == 0)
            {
                Console.Error.WriteLine("Certificate not found on smart card.");
                return;
            }
            cert = certs[0];
        }

        if (!cert.HasPrivateKey)
        {
            Console.Error.WriteLine("The selected certificate does not contain a private key.");
            return;
        }

        // Create a PKCS#7 object that Aspose.Pdf will use for signing.
        // Use the overload that accepts a PFX file path and password.
        PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);

        // Optional: customise the visual appearance of the signature.
        pkcs7.CustomAppearance = new SignatureCustomAppearance
        {
            BackgroundColor = Color.LightGray,
            ShowContactInfo = true,
            ShowLocation = true,
            ShowReason = true,
            ContactInfoLabel = "Contact:",
            LocationLabel = "Location:",
            ReasonLabel = "Reason:"
        };

        // Open the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the visible signature will be placed.
            Aspose.Pdf.Rectangle signatureRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

            // Create a signature field and add it to the document's form collection.
            SignatureField sigField = new SignatureField(doc, signatureRect)
            {
                Name = "Signature1"
            };
            doc.Form.Add(sigField);

            // Sign the field. The overload without extra parameters signs the field using the PKCS7 object.
            sigField.Sign(pkcs7);

            // Save the signed PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPath}'.");
    }
}
