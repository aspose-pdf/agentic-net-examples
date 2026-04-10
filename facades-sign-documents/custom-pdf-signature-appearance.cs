using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath  = "certificate.pfx";
        const string certPassword = "password";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdf))
        {
            // Initialize the facade for signing
            Aspose.Pdf.Facades.PdfFileSignature signer = new Aspose.Pdf.Facades.PdfFileSignature();
            signer.BindPdf(doc);

            // Create a PKCS#7 signature object with the certificate
            Aspose.Pdf.Forms.PKCS7 pkcs7 = new Aspose.Pdf.Forms.PKCS7(certPath, certPassword);
            pkcs7.Reason      = "I agree";
            pkcs7.ContactInfo = "contact@example.com";
            pkcs7.Location    = "New York, USA";

            // Create a custom appearance and hide the default caption
            Aspose.Pdf.Forms.SignatureCustomAppearance customAppearance = new Aspose.Pdf.Forms.SignatureCustomAppearance();
            customAppearance.DigitalSignedLabel = "";          // Hide "Digitally signed by"
            // Optionally hide other fields:
            // customAppearance.ShowReason = false;
            // customAppearance.ShowLocation = false;
            // customAppearance.ShowContactInfo = false;

            // Assign the custom appearance to the signature object
            pkcs7.CustomAppearance = customAppearance;

            // Define the signature rectangle (System.Drawing.Rectangle is required)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Sign page 1 (pages are 1‑based). The signature will be visible.
            signer.Sign(
                page:   1,
                SigReason:  pkcs7.Reason,
                SigContact: pkcs7.ContactInfo,
                SigLocation: pkcs7.Location,
                visible: true,
                annotRect: rect,
                sig: pkcs7);

            // Save the signed PDF
            signer.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}