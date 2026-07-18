using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_timestamp.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";
        const string tsaUrl = "https://tsa.example.com";
        const string tsaCredentials = "user:pass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the visible signature (llx, lly, urx, ury)
            Rectangle rect = new Rectangle(100, 100, 250, 150);

            // Create a signature field and add it to the first page of the document
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField, 1);

            // Create a PKCS7 signature object using the certificate
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason = "Signed with timestamp",
                ContactInfo = "contact@example.com",
                Location = "Head Office"
            };

            // Configure timestamp settings
            TimestampSettings tsSettings = new TimestampSettings(
                tsaUrl,
                tsaCredentials,
                DigestHashAlgorithm.Sha256);
            pkcs7.TimestampSettings = tsSettings;

            // Sign the signature field
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPath}'.");
    }
}
