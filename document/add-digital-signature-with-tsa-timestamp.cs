using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_output.pdf"; // signed PDF
        const string pfxPath    = "certificate.pfx";   // signing certificate
        const string pfxPassword = "pfxPassword";      // certificate password
        const string tsaUrl     = "https://tsa.example.com"; // trusted TSA URL
        const string tsaCredentials = "tsaUser:tsaPass";    // basic auth "user:pass"

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a signature field on the first page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            // Add the field to the document's form
            doc.Form.Add(sigField);

            // Prepare a PKCS#7 signature object
            PKCS7 pkcs7 = new PKCS7
            {
                Reason      = "Approved",
                Location    = "Head Office",
                ContactInfo = "contact@example.com"
            };

            // Configure timestamp settings (TSA)
            pkcs7.TimestampSettings = new TimestampSettings(
                serverUrl:          tsaUrl,
                basicAuthCredentials: tsaCredentials,
                digestHashAlgorithm: DigestHashAlgorithm.Sha256);

            // Sign the field using the certificate (PFX) stream
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                sigField.Sign(pkcs7, pfxStream, pfxPassword);
            }

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}