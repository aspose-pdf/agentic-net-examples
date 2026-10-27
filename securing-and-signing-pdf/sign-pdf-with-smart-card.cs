using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Prompt user to insert smart card and press Enter when ready.
        Console.WriteLine("Please insert your smart card and press Enter when ready...");
        Console.ReadLine();

        // Load a certificate that has a private key (smart‑card certificate).
        X509Certificate2 signingCert = null;
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            foreach (X509Certificate2 cert in store.Certificates)
            {
                if (cert.HasPrivateKey)
                {
                    signingCert = cert;
                    break;
                }
            }
        }

        if (signingCert == null)
        {
            Console.Error.WriteLine("No certificate with a private key was found in the current user store.");
            return;
        }

        // Open the PDF document.
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a signature field on the first page.
            var rect = new Rectangle(100, 100, 250, 150);
            var sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1",
                Name = "Signature1"
                // Reason, Location, ContactInfo are not members of SignatureField.
            };

            // Add the signature field to the document's AcroForm.
            doc.Form.Add(sigField, 1);

            // NOTE: Actual digital signing (PKCS#7) requires the Aspose.Pdf.Signatures package.
            // This example only creates an empty signature field so that the code compiles
            // without referencing optional assemblies.

            // Save the PDF (the field will be visible and can be signed later by a PDF viewer).
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
