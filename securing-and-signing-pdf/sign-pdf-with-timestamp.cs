using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security; // for DigestHashAlgorithm (enum is in Aspose.Pdf namespace, but this using is safe)

class Program
{
    static void Main()
    {
        // Input PDF, output signed PDF, certificate (PFX) and TSA server details
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "pfxPassword";

        // Trusted Time‑Stamp Authority (TSA) server URL and optional basic auth credentials
        const string tsaServerUrl = "https://freetsa.org/tsr"; // example TSA
        const string tsaBasicAuth = "username:password";      // leave empty if not required

        // Ensure the input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a signature field on the first page (adjust rectangle as needed)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);
            SignatureField sigField = new SignatureField(pdfDoc.Pages[1], sigRect)
            {
                PartialName = "Signature1"
            };
            pdfDoc.Form.Add(sigField);

            // Prepare the PKCS#7 signature object using the certificate
            PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword)
            {
                Reason   = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // Configure timestamp settings (SHA‑256 is the default, can be omitted)
            pkcs7Signature.TimestampSettings = new TimestampSettings(
                serverUrl: tsaServerUrl,
                basicAuthCredentials: tsaBasicAuth,
                digestHashAlgorithm: DigestHashAlgorithm.Sha256);

            // Sign the document using the signature field
            sigField.Sign(pkcs7Signature);

            // Save the signed PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPdfPath}'.");
    }
}