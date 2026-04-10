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

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Define the signature field rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

            // Create a signature field on the first page
            SignatureField signatureField = new SignatureField(doc, rect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(signatureField);

            // Open the personal certificate store and retrieve certificates (smart‑card certificates will trigger PIN entry)
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection availableCerts = store.Certificates;
            store.Close();

            // Prompt the user to select a certificate
            X509Certificate2Collection selected = X509Certificate2UI.SelectFromCollection(
                availableCerts,
                "Select Certificate",
                "Choose a certificate stored on a smart card",
                X509SelectionFlag.SingleSelection);

            if (selected == null || selected.Count == 0)
            {
                Console.Error.WriteLine("No certificate selected.");
                return;
            }

            X509Certificate2 cert = selected[0];

            // Create an ExternalSignature (detached PKCS#7) using the selected certificate
            ExternalSignature externalSignature = new ExternalSignature(cert)
            {
                Reason = "Document approval",
                Location = Environment.MachineName,
                ContactInfo = Environment.UserName,
                Date = DateTime.UtcNow
            };

            // Sign the PDF using the signature field
            signatureField.Sign(externalSignature);

            // Save the signed PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
