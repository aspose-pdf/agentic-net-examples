using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class SignPdfWithTimestamp
{
    static void Main()
    {
        // Input PDF, output PDF, and signing certificate (PFX) details
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_with_timestamp.pdf";
        const string pfxPath        = "certificate.pfx";
        const string pfxPassword    = "pfxPassword";

        // Timestamp Authority (TSA) server URL and optional basic authentication credentials
        const string tsaServerUrl          = "https://tsa.example.com";
        const string tsaBasicAuthCreds     = ""; // format "username:password" or empty if not required

        // Ensure the input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a signature field on the first page (coordinates are in points)
            // Use fully qualified Rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField signatureField = new SignatureField(doc.Pages[1], sigRect);
            doc.Form.Add(signatureField);

            // Initialize a PKCS#7 signature object using the PFX certificate
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword);

            // Configure timestamp settings (server URL, optional basic auth, hash algorithm)
            pkcs7Signature.TimestampSettings = new TimestampSettings(
                tsaServerUrl,
                tsaBasicAuthCreds,
                DigestHashAlgorithm.Sha256);

            // Optional: set additional signature properties (reason, location, etc.)
            pkcs7Signature.Reason   = "Document approved";
            pkcs7Signature.Location = "Company HQ";

            // Sign the document using the signature field
            signatureField.Sign(pkcs7Signature);

            // Save the signed PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed and timestamped successfully: {outputPdfPath}");
    }
}