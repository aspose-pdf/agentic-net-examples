using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "signed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // Define the rectangle where the signature field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field and add it to the form fields collection
            SignatureField signatureField = new SignatureField(page, rect)
            {
                Name = "Signature1"
            };
            doc.Form.Add(signatureField);

            // Retrieve a certificate with a private key from the smart card.
            // The OS will prompt for the PIN when the private key is accessed.
            X509Certificate2 certificate = GetCertificateFromSmartCard();

            if (certificate == null)
            {
                Console.Error.WriteLine("No certificate with a private key was found on the smart card.");
                return;
            }

            // Create an ExternalSignature that uses the smart‑card certificate.
            ExternalSignature externalSignature = new ExternalSignature(certificate)
            {
                Reason      = "Document signed with smart card",
                Location    = Environment.MachineName,
                ContactInfo = "user@example.com"
            };

            // Sign the PDF using the signature field (lifecycle rule: use Sign method)
            signatureField.Sign(externalSignature);

            // Save the signed PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }

    // Helper method to obtain the first certificate with a private key from the CurrentUser store.
    // Adjust the selection logic as needed (e.g., by subject name or thumbprint).
    private static X509Certificate2 GetCertificateFromSmartCard()
    {
        X509Certificate2 result = null;
        X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        try
        {
            store.Open(OpenFlags.ReadOnly);
            foreach (X509Certificate2 cert in store.Certificates)
            {
                if (cert.HasPrivateKey)
                {
                    result = cert;
                    break; // Use the first matching certificate
                }
            }
        }
        finally
        {
            store.Close();
        }
        return result;
    }
}