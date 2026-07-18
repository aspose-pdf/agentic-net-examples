using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "signed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Prompt for PIN – the smart‑card driver will request it when the private key is accessed.
        Console.Write("Enter smart card PIN: ");
        string pin = Console.ReadLine(); // PIN is not used directly; kept for user interaction.

        // Retrieve a certificate that has a private key from the current user store (smart‑card).
        X509Certificate2 cert = null;
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            foreach (var c in store.Certificates)
            {
                if (c.HasPrivateKey)
                {
                    cert = c;
                    break;
                }
            }
        }

        if (cert == null)
        {
            Console.Error.WriteLine("No certificate with a private key found in the store.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal).
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the signature will appear.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the first page and add it to the page annotations.
            SignatureField signatureField = new SignatureField(doc.Pages[1], rect);
            doc.Pages[1].Annotations.Add(signatureField);

            // Create an ExternalSignature that uses the smart‑card certificate.
            ExternalSignature externalSignature = new ExternalSignature(cert)
            {
                Reason      = "Signed with smart card",
                Location    = "Office",
                ContactInfo = "user@example.com"
            };

            // Sign the PDF using the signature field.
            signatureField.Sign(externalSignature);

            // Save the signed PDF (lifecycle rule: use Document.Save).
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}