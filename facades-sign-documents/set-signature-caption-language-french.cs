using System;
using System.IO;
using System.Drawing;
using System.Globalization;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";
        const string signatureImagePath = "signature.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Initialize the PDF signer facade
        PdfFileSignature pdfSigner = new PdfFileSignature();
        pdfSigner.BindPdf(inputPdf);
        pdfSigner.SignatureAppearance = signatureImagePath;

        // Create a PKCS7 signature and set its metadata
        PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword);
        pkcs7Signature.Reason = "Approved";
        pkcs7Signature.ContactInfo = "contact@example.com";
        pkcs7Signature.Location = "Paris";

        // Customize the appearance locale to French
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
        customAppearance.Culture = new CultureInfo("fr-FR");
        pkcs7Signature.CustomAppearance = customAppearance;

        // Define the signature rectangle (x, y, width, height)
        Rectangle signatureRect = new Rectangle(100, 100, 200, 100);

        // Apply the signature on the first page
        pdfSigner.Sign(1, true, signatureRect, pkcs7Signature);

        // Save the signed PDF
        pdfSigner.Save(outputPdf);
        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}