using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfFileSignature
using Aspose.Pdf.Forms;           // PKCS1, SignatureCustomAppearance

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

        if (!File.Exists(inputPdf) || !File.Exists(certPath))
        {
            Console.Error.WriteLine("Input PDF or certificate file not found.");
            return;
        }

        // Bind the PDF, configure the signature, and save.
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Load the PDF to be signed.
            pdfSign.BindPdf(inputPdf);

            // Create a PKCS#1 signature object.
            PKCS1 signature = new PKCS1(certPath, certPass);

            // Hide the default "Digitally signed by" caption and other default fields.
            signature.ShowProperties = false; // disables the built‑in label set.

            // Optional: further customize appearance (e.g., remove any remaining labels).
            SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
            customAppearance.DigitalSignedLabel = ""; // ensure the caption is empty.
            customAppearance.ShowReason        = false;
            customAppearance.ShowLocation      = false;
            customAppearance.ShowContactInfo   = false;
            signature.CustomAppearance = customAppearance;

            // Define the visible rectangle for the signature (System.Drawing.Rectangle).
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Sign page 1, make the signature visible, using the custom appearance.
            pdfSign.Sign(page: 1, visible: true, annotRect: rect, sig: signature);

            // Save the signed PDF.
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}