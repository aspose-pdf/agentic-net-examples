using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        try
        {
            PdfFileSignature pdfSignature = new PdfFileSignature();
            pdfSignature.BindPdf(inputPath);
            pdfSignature.SetCertificate(certificatePath, certificatePassword);

            PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword);
            pkcs7Signature.Reason = "Document approved";
            pkcs7Signature.ContactInfo = "john.doe@example.com";
            pkcs7Signature.Location = "London";

            System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 50);
            pdfSignature.Sign(1, true, signatureRect, pkcs7Signature);
            pdfSignature.Save(outputPath);
            Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}