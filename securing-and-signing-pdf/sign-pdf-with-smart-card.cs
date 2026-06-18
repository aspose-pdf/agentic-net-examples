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
        const string outputPath = "signed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the signature field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Add a signature field to the first page
            SignatureField signatureField = new SignatureField(doc, rect);
            doc.Pages[1].Annotations.Add(signatureField);

            // Retrieve a certificate with a private key from the smart card
            X509Certificate2 cert = GetCertificateFromSmartCard();
            if (cert == null)
            {
                Console.Error.WriteLine("No suitable certificate with a private key found on the smart card.");
                return;
            }

            // Create an ExternalSignature using the certificate.
            // Accessing the private key will trigger the OS PIN prompt.
            ExternalSignature externalSignature = new ExternalSignature(cert)
            {
                Reason = "Document approval",
                Location = Environment.MachineName,
                ContactInfo = Environment.UserName
            };

            // Sign the PDF using the signature field
            signatureField.Sign(externalSignature);

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }

    // Helper: selects the first certificate in the current user's "My" store that has a private key.
    // Accessing the private key forces the OS to request the smart card PIN.
    static X509Certificate2 GetCertificateFromSmartCard()
    {
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            foreach (X509Certificate2 cert in store.Certificates)
            {
                if (cert.HasPrivateKey)
                {
                    try
                    {
                        // Attempt to acquire the private key; this will prompt for the PIN if needed.
                        var key = cert.GetRSAPrivateKey();
                        if (key != null)
                        {
                            return cert;
                        }
                    }
                    catch (Exception ex)
                    {
                        // If the user cancels the PIN prompt or another error occurs, continue searching.
                        Console.Error.WriteLine($"Unable to access private key: {ex.Message}");
                    }
                }
            }
        }
        return null;
    }
}