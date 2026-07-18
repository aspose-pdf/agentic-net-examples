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
        const string certPath   = "certificate.pfx";   // PKCS#12 certificate
        const string certPass   = "password";          // certificate password

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Add a signature field (visible) on the first page
            // -----------------------------------------------------------------
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);
            SignatureField sigField = new SignatureField(page, sigRect)
            {
                PartialName = "Signature1" // field name
            };
            page.Annotations.Add(sigField);

            // -----------------------------------------------------------------
            // 2. Create a PKCS#1 signature object and set its properties
            // -----------------------------------------------------------------
            PKCS1 pkcs1Signature = new PKCS1(certPath, certPass)
            {
                Reason      = "Document approved",
                ContactInfo = "john.doe@example.com",
                Location    = "New York"
            };

            // -----------------------------------------------------------------
            // 3. Sign the document using the signature field
            // -----------------------------------------------------------------
            sigField.Sign(pkcs1Signature);

            // -----------------------------------------------------------------
            // 4. Embed custom XMP metadata describing the signing process
            // -----------------------------------------------------------------
            // Document.Metadata is the core‑API XMP metadata container.
            // Add a custom property; the namespace prefix "custom" is arbitrary.
            doc.Metadata.Add("custom:SigningProcess", "Signed using Aspose.Pdf core API (PKCS#1).");

            // -----------------------------------------------------------------
            // 5. Save the signed PDF
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}