using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security; // TimestampSettings, DigestHashAlgorithm are in Aspose.Pdf namespace; this using is optional but kept for clarity

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputPdfPath = "signed_output.pdf";  // signed PDF
        const string pfxPath       = "certificate.pfx";   // signing certificate
        const string pfxPassword   = "pfxPassword";       // certificate password
        const string tsaUrl        = "https://tsa.example.com"; // trusted TSA URL
        const string tsaCredentials = "username:password";      // basic auth for TSA

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a signature field on the first page (pages are 1‑based)
            // Fully qualify Rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 200, 150);
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField);

            // Prepare a PKCS#7 signature object
            PKCS7 pkcs7 = new PKCS7
            {
                ContactInfo = "contact@example.com",
                Location    = "Office",
                Reason      = "Document signed with trusted timestamp"
            };

            // Configure timestamp settings (TimestampSettings constructor)
            pkcs7.TimestampSettings = new TimestampSettings(
                serverUrl:            tsaUrl,
                basicAuthCredentials: tsaCredentials,
                digestHashAlgorithm:  DigestHashAlgorithm.Sha256);

            // Sign the field using the certificate (PFX stream)
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                sigField.Sign(pkcs7, pfxStream, pfxPassword);
            }

            // Save the signed PDF (using rule: save-to-non-pdf-always-use-save-options is not needed here because we save as PDF)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}