using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class SignPdfWithTimestamp
{
    static void Main()
    {
        // Input PDF, output PDF, certificate (PFX) and TSA server details
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "pfxPassword";

        // TSA server URL and optional basic authentication (username:password)
        const string tsaServerUrl = "https://freetsa.org/tsr"; // example TSA
        const string tsaAuthCredentials = ""; // leave empty if no authentication

        // Verify input files exist
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a signature field on the first page.
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField signatureField = new SignatureField(pdfDocument.Pages[1], sigRect);
            pdfDocument.Form.Add(signatureField);

            // Create a PKCS#7 signature object using the certificate.
            PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword)
            {
                Reason   = "Document approved",
                Location = "Company HQ",
                ContactInfo = "contact@example.com",
                // Optional: set a custom appearance or other properties here.
            };

            // Configure timestamp settings.
            // DigestHashAlgorithm defaults to SHA256; explicitly set if desired.
            TimestampSettings tsSettings = new TimestampSettings(
                serverUrl: tsaServerUrl,
                basicAuthCredentials: tsaAuthCredentials,
                digestHashAlgorithm: DigestHashAlgorithm.Sha256);

            pkcs7Signature.TimestampSettings = tsSettings;

            // Sign the document using the signature field.
            signatureField.Sign(pkcs7Signature);

            // Save the signed PDF.
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed and timestamped successfully: {outputPdfPath}");
    }
}