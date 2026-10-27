using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_sha384.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

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

        using (Document doc = new Document(inputPath))
        {
            // Create an invisible signature field on the first page
            var rect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);
            var sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField);

            // Prepare PKCS7 signature
            var pkcs = new PKCS7(certPath, certPassword);

            // Set SHA‑384 as the digest algorithm via TimestampSettings
            pkcs.TimestampSettings = new TimestampSettings(
                "http://example.com",          // TSA server URL (dummy)
                null,                         // no authentication
                DigestHashAlgorithm.Sha384);  // digest algorithm

            // Sign the field
            sigField.Sign(pkcs);

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document signed with SHA‑384 saved to '{outputPath}'.");
    }
}
