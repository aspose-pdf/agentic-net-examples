using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using System.Drawing;

class Program
{
    static void Main()
    {
        string inputPdf = "input.pdf";
        string outputPdf = "signed_output.pdf";
        string certificatePath = "certificate.pfx";
        string certificatePassword = "password";

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

        try
        {
            PdfFileSignature signer = new PdfFileSignature();
            signer.BindPdf(inputPdf);

            // Optional: set a visual appearance image for the signature
            // signer.SignatureAppearance = "signature_image.png";

            PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword);
            pkcs7.Reason = "Document approved";
            pkcs7.ContactInfo = "contact@example.com";
            pkcs7.Location = "Office";

            System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 50);
            signer.Sign(1, true, signatureRect, pkcs7);
            signer.Save(outputPdf);

            Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}