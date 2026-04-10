using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class SignPdfWithRsa4096
{
    static void Main()
    {
        // Paths to the input PDF, the RSA 4096‑bit certificate (PFX) and the output PDF
        const string inputPdfPath   = "input.pdf";
        const string pfxPath        = "certificate_4096.pfx";
        const string pfxPassword    = "yourPfxPassword";
        const string outputPdfPath  = "signed_output.pdf";

        // Ensure the required files exist
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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Choose the page where the signature field will be placed (first page in this example)
            Page page = pdfDocument.Pages[1];

            // Define the rectangle (llx, lly, urx, ury) for the visual appearance of the signature
            Aspose.Pdf.Rectangle signatureRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field and add it to the page's annotations collection
            SignatureField signatureField = new SignatureField(page, signatureRect)
            {
                PartialName = "Signature1" // optional name for the field
            };
            page.Annotations.Add(signatureField);

            // Create a PKCS#1 signature object using the RSA 4096‑bit certificate
            // The PKCS1 constructor that takes (string pfx, string password) loads the certificate
            PKCS1 pkcs1Signature = new PKCS1(pfxPath, pfxPassword)
            {
                // Optional metadata for the signature appearance
                Reason   = "Document approved",
                Location = "Head Office",
                ContactInfo = "contact@example.com",
                Date = DateTime.Now
            };

            // Sign the document using the signature field
            signatureField.Sign(pkcs1Signature);

            // Save the signed PDF (lifecycle rule: use Document.Save)
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}