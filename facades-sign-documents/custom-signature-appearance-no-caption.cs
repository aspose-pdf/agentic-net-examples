using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfFileSignature
using Aspose.Pdf.Forms;           // PKCS1, SignatureCustomAppearance

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";
        const string outputPdf     = "signed.pdf";
        const string certificate   = "certificate.pfx";
        const string certPassword  = "password";

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(certificate))
        {
            Console.Error.WriteLine("Input PDF or certificate file not found.");
            return;
        }

        // Bind the PDF to the signature facade
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // Create a PKCS#1 signature object
            PKCS1 pkcs1 = new PKCS1(certificate, certPassword);

            // ----- Custom appearance: hide the default "Digitally signed by" caption -----
            SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
            customAppearance.DigitalSignedLabel = string.Empty; // empty label removes the caption
            pkcs1.CustomAppearance = customAppearance;
            // ---------------------------------------------------------------------------

            // Define the visible signature rectangle (System.Drawing.Rectangle is required)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 50);

            // Sign page 1 with reason, contact, location and the custom appearance
            pdfSignature.Sign(
                page: 1,
                SigReason: "Approved",
                SigContact: "john.doe@example.com",
                SigLocation: "New York",
                visible: true,
                annotRect: rect,
                sig: pkcs1);

            // Save the signed PDF
            pdfSignature.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}