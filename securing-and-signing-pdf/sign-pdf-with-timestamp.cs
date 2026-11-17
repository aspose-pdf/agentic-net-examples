using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, output PDF, and signing certificate (PKCS#12)
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "certPassword";

        // URL of the trusted Time‑Stamp Authority (TSA) server
        const string tsaUrl = "http://timestamp.example.com";

        // Verify that required files exist
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
        Document pdfDoc = new Document(inputPdfPath);

        // Define the rectangle where the signature appearance will be placed
        // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
        Rectangle signatureRect = new Rectangle(100, 100, 250, 150);

        // Create a signature field on the first page
        SignatureField signatureField = new SignatureField(pdfDoc.Pages[1], signatureRect)
        {
            PartialName = "Signature1"
        };

        // Add the signature field to the document's form collection
        pdfDoc.Form.Add(signatureField, 1);

        // Prepare the PKCS7 object with certificate and timestamp settings
        PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword)
        {
            Reason = "Approved for release",
            Location = "New York, USA",
            ContactInfo = "contact@example.com",
            TimestampSettings = new TimestampSettings(tsaUrl, null, DigestHashAlgorithm.Sha256)
        };

        // Sign the PDF using the PKCS7 object (timestamp will be applied automatically)
        signatureField.Sign(pkcs7);

        // Save the signed PDF
        pdfDoc.Save(outputPdfPath);

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}
