using System;
using System.IO;
using System.Drawing; // required for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certificate = "certificate.pfx";
        const string certPassword = "password";
        const string signatureImg = "signature.png";

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(certificate) || !File.Exists(signatureImg))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Load the PDF document and create the signer
        using (Document doc = new Document(inputPdf))
        using (PdfFileSignature signer = new PdfFileSignature())
        {
            // Bind the document to the signer
            signer.BindPdf(doc);

            // Set the visual signature image (this replaces the non‑existent SignatureAppearance class)
            signer.SignatureAppearance = signatureImg;

            // Create PKCS7 credentials and customise its appearance
            PKCS7 pkcs = new PKCS7(certificate, certPassword)
            {
                Reason = "Approved",
                Location = "New York, USA",
                ContactInfo = "contact@example.com"
            };

            // Hide the default "Digitally signed by" caption and any other default labels
            pkcs.CustomAppearance = new SignatureCustomAppearance
            {
                DigitalSignedLabel = string.Empty, // hides "Digitally signed by"
                ShowContactInfo = false,
                ShowLocation = false,
                ShowReason = false,
                IsForegroundImage = true
            };

            // PdfFileSignature.Sign expects a System.Drawing.Rectangle (x, y, width, height)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 150, 50);

            // Apply the signature to page 1 (page numbers are 1‑based)
            signer.Sign(1, true, rect, pkcs);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
