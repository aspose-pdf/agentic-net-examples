using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string signatureImage = "signature.png";
        const string certPath = "cert.pfx";          // path to a valid certificate (PKCS#12)
        const string certPassword = "password";     // certificate password

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(signatureImage))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImage}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Bind the PDF document to the signature facade (Facades API)
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // Create a PKCS7 object (required for signing). Even if you only need the visual
            // appearance, a PKCS7 instance must be supplied to the Sign method.
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                // Suppress default text fields (reason, location, contact) if not needed
                Reason = string.Empty,
                Location = string.Empty,
                ContactInfo = string.Empty,
                // Set up a custom appearance – we only need to tell the engine whether the
                // image should be drawn in front or behind the signature field.
                CustomAppearance = new SignatureCustomAppearance
                {
                    IsForegroundImage = true, // draw the image on top of the field background
                    FontFamilyName = "Arial"
                }
            };

            // Assign the image that will be used as the visual signature background.
            // The image is not part of SignatureCustomAppearance; it is set on the facade.
            pdfSignature.SignatureAppearance = signatureImage;

            // Sign the field named "Signature" with the prepared PKCS7 object.
            // The field must already exist in the PDF; otherwise an exception will be thrown.
            pdfSignature.Sign("Signature", pkcs7);

            // Save the updated PDF
            pdfSignature.Save(outputPdf);
        }

        Console.WriteLine($"Custom signature appearance saved to '{outputPdf}'.");
    }
}
