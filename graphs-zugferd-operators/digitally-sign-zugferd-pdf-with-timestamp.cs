using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // PKCS7 and TimestampSettings are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf = "invoice.pdf";          // ZUGFeRD PDF to be signed
        const string outputPdf = "invoice_signed.pdf";  // Resulting signed PDF
        const string pfxPath = "certificate.pfx";      // PFX certificate file
        const string pfxPassword = "password";         // Password for the PFX
        const string timestampUrl = "http://timestamp.digicert.com"; // TSA URL

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(pfxPath))
        {
            Console.Error.WriteLine("Input PDF or PFX file not found.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the signature appearance will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField signatureField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1",
                // Optional visual styling
                Color = Aspose.Pdf.Color.LightGray
            };

            // Add the signature field to the document's form collection (page number = 1)
            doc.Form.Add(signatureField, 1);

            // Create a PKCS#7 signature object using the PFX file and password
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Approved",
                Location = "Company HQ",
                ContactInfo = "contact@example.com",
                Date = DateTime.UtcNow
            };

            // Configure timestamp settings (adds a trusted timestamp to the signature)
            // TimestampSettings constructor requires serverUrl, username, and digest algorithm.
            TimestampSettings ts = new TimestampSettings(timestampUrl, "", DigestHashAlgorithm.Sha256);
            pkcs7.TimestampSettings = ts;

            // Sign the signature field with the prepared PKCS#7 signature
            signatureField.Sign(pkcs7);

            // Save the signed PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
