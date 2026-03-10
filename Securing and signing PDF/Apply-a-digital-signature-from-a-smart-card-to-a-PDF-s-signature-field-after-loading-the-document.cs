using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the signature field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a new signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                PartialName = "Signature1"
            };

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField);

            // Prompt the user to select a certificate (e.g., from a smart card)
            X509Certificate2Collection certs = X509Certificate2UI.SelectFromCollection(
                new X509Store(StoreName.My, StoreLocation.CurrentUser).Certificates,
                "Select Certificate",
                "Select a certificate for signing",
                X509SelectionFlag.SingleSelection);

            if (certs == null || certs.Count == 0)
            {
                Console.Error.WriteLine("No certificate selected. Signing aborted.");
                return;
            }

            X509Certificate2 cert = certs[0];

            // Create an ExternalSignature using the selected certificate
            ExternalSignature externalSig = new ExternalSignature(cert)
            {
                Reason      = "Document approved",
                ContactInfo = "user@example.com",
                Location    = "Office"
            };

            // Apply the digital signature to the signature field
            sigField.Sign(externalSig);

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}