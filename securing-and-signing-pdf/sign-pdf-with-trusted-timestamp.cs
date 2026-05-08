using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        // Trusted Time‑Stamp Authority (TSA) server URL and basic authentication (username:password)
        const string tsaUrl = "https://tsa.example.com";
        const string tsaCredentials = "username:password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the signature appearance will be placed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Add a signature field to the first page via the Fields collection
            Page firstPage = doc.Pages[1];
            SignatureField sigField = new SignatureField(firstPage, rect)
            {
                PartialName = "Signature1"
            };
            // The Add method registers the field with the document form
            doc.Form.Add(sigField, 1);

            // Create a PKCS#7 signature object using the certificate
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // Configure timestamp settings to obtain a trusted timestamp from the TSA
            pkcs7.TimestampSettings = new TimestampSettings(
                serverUrl: tsaUrl,
                basicAuthCredentials: tsaCredentials,
                digestHashAlgorithm: DigestHashAlgorithm.Sha256);

            // Sign the PDF using the signature field
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
