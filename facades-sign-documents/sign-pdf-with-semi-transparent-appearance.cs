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
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";
        const string signatureImagePath = "signatureImage.png"; // optional appearance image

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Initialize the facade for signing
        PdfFileSignature pdfSigner = new PdfFileSignature();
        pdfSigner.BindPdf(inputPdf);

        // Load the certificate (required for PKCS7)
        pdfSigner.SetCertificate(certificatePath, certificatePassword);

        // Create a PKCS7 signature object (PKCS1 does not expose ContactInfo, Reason, etc.)
        PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword);
        pkcs7Signature.Reason = "Document approved";
        pkcs7Signature.ContactInfo = "john.doe@example.com"; // correct property name
        pkcs7Signature.Location = "New York";

        // Configure a custom appearance with a semi‑transparent (light) background color
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
        // Light yellow background – improves readability; Aspose.Pdf.Color does not expose alpha,
        // so we use a pale color to simulate semi‑transparency.
        customAppearance.BackgroundColor = Color.FromRgb(255, 255, 200);
        customAppearance.ForegroundColor = Color.Blue; // optional text color
        pkcs7Signature.CustomAppearance = customAppearance;

        // Optional: set an image that will be shown behind the signature fields
        pdfSigner.SignatureAppearance = signatureImagePath;

        // Define the rectangle where the signature will be placed (System.Drawing.Rectangle is required)
        System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Sign the first page, make the signature visible
        pdfSigner.Sign(1, true, signatureRect, pkcs7Signature);

        // Save the signed PDF
        pdfSigner.Save(outputPdf);
        pdfSigner.Close();

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
