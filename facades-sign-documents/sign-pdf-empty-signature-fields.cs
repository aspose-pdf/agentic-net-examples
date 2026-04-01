using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // <-- added namespace for PKCS7

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
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine("Certificate file not found: " + certificatePath);
            return;
        }

        PdfFileSignature pdfSign = new PdfFileSignature();
        pdfSign.BindPdf(inputPath);
        pdfSign.SetCertificate(certificatePath, certificatePassword);

        // Create PKCS7 object for digital signature
        PKCS7 signature = new PKCS7(certificatePath, certificatePassword);
        // Suppress location, reason and contact information by setting empty strings
        signature.Location = string.Empty;
        signature.Reason = string.Empty;
        // The PKCS7 class uses the property name 'ContactInfo' for contact details
        signature.ContactInfo = string.Empty;

        Rectangle rect = new Rectangle(100, 100, 200, 100);
        pdfSign.Sign(1, true, rect, signature);
        pdfSign.Save(outputPath);

        Console.WriteLine("PDF signed and saved to '" + outputPath + "'.");
    }
}
