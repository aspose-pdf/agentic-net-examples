using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string thumbprint = "YOUR_CERTIFICATE_THUMBPRINT"; // replace with actual thumbprint (no spaces, case‑insensitive)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the certificate from the CurrentUser\My store by thumbprint
        X509Certificate2 signingCert = null;
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            var certs = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
            signingCert = certs.OfType<X509Certificate2>().FirstOrDefault();
        }

        if (signingCert == null)
        {
            Console.Error.WriteLine($"Certificate with thumbprint '{thumbprint}' not found in the store.");
            return;
        }

        // Open the PDF, add a signature field, and sign it
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the visible signature will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                // Optional: give the field a name
                Name = "Signature1"
            };

            // Add the signature field to the page's annotations collection
            doc.Pages[1].Annotations.Add(sigField);

            // Create an ExternalSignature that uses the X509Certificate2 from the store
            ExternalSignature externalSig = new ExternalSignature(signingCert)
            {
                Reason   = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // Sign the field with the external signature
            sigField.Sign(externalSig);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdf}'.");
    }
}