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

        // Prompt the user for the smart‑card PIN (if the provider requires it)
        Console.Write("Enter smart card PIN: ");
        string pin = Console.ReadLine(); // PIN can be used by the crypto provider internally

        // Retrieve a certificate that has a private key from the current user's store
        X509Certificate2 certificate = null;
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            foreach (X509Certificate2 cert in store.Certificates)
            {
                if (cert.HasPrivateKey)
                {
                    certificate = cert;
                    break;
                }
            }
        }

        if (certificate == null)
        {
            Console.Error.WriteLine("No certificate with a private key was found on the smart card.");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Create a signature field on the first page (if one does not already exist)
            const string signatureFieldName = "Signature1";
            var rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);
            SignatureField sigField = new SignatureField(doc, rect);
            sigField.Name = signatureFieldName;
            doc.Form.Add(sigField);

            // Build an external signature that uses the smart‑card certificate
            ExternalSignature externalSig = new ExternalSignature(certificate);
            externalSig.Reason = "Approved";
            externalSig.ContactInfo = "user@example.com";
            externalSig.Location = "Head Office";

            // Sign the field
            sigField.Sign(externalSig);

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
