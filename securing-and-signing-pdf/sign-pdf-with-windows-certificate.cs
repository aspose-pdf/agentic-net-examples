using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string thumbprint = "YOUR_CERTIFICATE_THUMBPRINT"; // replace with actual thumbprint

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the certificate from the Windows certificate store
        X509Certificate2 certificate = GetCertificateFromStore(thumbprint);
        if (certificate == null)
        {
            Console.Error.WriteLine($"Certificate with thumbprint '{thumbprint}' not found.");
            return;
        }

        // Load the PDF, add a signature field, sign it, and save
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the signature appearance (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], rect);
            // Assign a name to the field (the Form.Add overload expects only the field)
            sigField.PartialName = "Signature1";
            doc.Form.Add(sigField);

            // Create an ExternalSignature using the X509Certificate2 (detached PKCS#7)
            ExternalSignature externalSig = new ExternalSignature(certificate);

            // Sign the field
            sigField.Sign(externalSig);

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }

    // Helper method to retrieve a certificate by thumbprint from the current user or local machine store
    static X509Certificate2 GetCertificateFromStore(string thumbprint)
    {
        StoreLocation[] locations = { StoreLocation.CurrentUser, StoreLocation.LocalMachine };
        foreach (StoreLocation location in locations)
        {
            using (X509Store store = new X509Store(StoreName.My, location))
            {
                store.Open(OpenFlags.ReadOnly);
                foreach (X509Certificate2 cert in store.Certificates)
                {
                    if (string.Equals(cert.Thumbprint, thumbprint, StringComparison.OrdinalIgnoreCase))
                    {
                        return cert;
                    }
                }
            }
        }
        return null; // No matching certificate found
    }
}