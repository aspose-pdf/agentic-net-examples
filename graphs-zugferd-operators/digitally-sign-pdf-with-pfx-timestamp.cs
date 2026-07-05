using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string pfxPath    = "certificate.pfx";
        const string pfxPassword = "pfxPassword";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"PFX file not found: {pfxPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Create a signature field on the first page.
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);
            SignatureField signatureField = new SignatureField(doc.Pages[1], rect)
            {
                // Set a name for the field (used as the form field identifier).
                PartialName = "Signature1"
            };

            // Add the signature field to the page's annotation collection.
            doc.Pages[1].Annotations.Add(signatureField);

            // Initialize the PKCS#7 signature object using the PFX file and password.
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword)
            {
                // Optional metadata for the signature.
                Reason      = "Document approved",
                Location    = "Head Office",
                ContactInfo = "contact@example.com",
                Date        = DateTime.Now,
                // Show default signature properties (subject, date, reason, location).
                ShowProperties = true
            };

            // Configure timestamp settings (replace with a valid TSA URL if needed).
            // TimestampSettings constructor requires three arguments: serverUrl, basicAuthCredentials, digest algorithm.
            pkcs7Signature.TimestampSettings = new TimestampSettings(
                "http://timestamp.example.com",
                string.Empty,
                DigestHashAlgorithm.Sha256);

            // Sign the document using the signature field.
            signatureField.Sign(pkcs7Signature);

            // Save the signed PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
