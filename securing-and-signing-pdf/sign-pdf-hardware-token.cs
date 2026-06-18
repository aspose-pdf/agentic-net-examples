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
            // Define the rectangle where the visible signature will appear
            // Parameters: llx, lly, urx, ury (lower‑left X/Y, upper‑right X/Y)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect);
            // Add the field to the document's form collection
            doc.Form.Add(sigField);

            // Retrieve a certificate from the hardware token / smart card.
            // This example selects the first certificate that has a private key.
            // Adjust the selection logic as needed for your environment.
            X509Certificate2 cert = null;
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);
                foreach (X509Certificate2 c in store.Certificates)
                {
                    if (c.HasPrivateKey)
                    {
                        cert = c;
                        break;
                    }
                }
                store.Close();
            }

            if (cert == null)
            {
                Console.Error.WriteLine("No suitable certificate with a private key was found in the current user's store.");
                return;
            }

            // Create an external signature object that works with non‑exportable private keys (e.g., hardware token)
            ExternalSignature externalSig = new ExternalSignature(cert);

            // Set optional signature metadata
            externalSig.Reason      = "Document approval";
            externalSig.Location    = "Head Office";
            externalSig.ContactInfo = "signer@example.com";

            // Sign the PDF using the signature field and the external signature
            sigField.Sign(externalSig);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdf}'.");
    }
}