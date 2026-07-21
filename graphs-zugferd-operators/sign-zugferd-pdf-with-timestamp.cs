using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "invoice_zugferd.pdf";   // ZUGFeRD PDF to be signed
        const string outputPdfPath  = "invoice_zugferd_signed.pdf";
        const string pfxPath        = "certificate.pfx";      // PFX containing signing certificate
        const string pfxPassword    = "pfxPassword";          // Password for the PFX file
        const string timestampUrl   = "http://timestamp.example.com"; // TSA URL

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"PFX file not found: {pfxPath}");
            return;
        }

        try
        {
            // Load the existing ZUGFeRD PDF
            using (Document doc = new Document(inputPdfPath))
            {
                // Choose the page where the signature will appear (first page)
                Page page = doc.Pages[1];

                // Define the rectangle for the signature field (coordinates in points)
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

                // Create a signature field and add it to the page annotations
                SignatureField signatureField = new SignatureField(page, sigRect);
                page.Annotations.Add(signatureField);

                // Create a concrete PKCS7 signature object (Signature is abstract)
                PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);
                pkcs7.Reason      = "Document approval";
                pkcs7.Location    = "Company HQ";
                pkcs7.ContactInfo = "support@example.com";
                pkcs7.Date        = DateTime.UtcNow;

                // Configure timestamp settings – the constructor requires URL, optional basic auth username, and hash algorithm
                pkcs7.TimestampSettings = new TimestampSettings(timestampUrl, "", DigestHashAlgorithm.Sha256);

                // Sign the field using the PKCS7 object
                signatureField.Sign(pkcs7);

                // Save the signed PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}
