using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine("Certificate file not found: " + certificatePath);
            return;
        }

        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword);
            SignatureCustomAppearance appearance = new SignatureCustomAppearance();
            appearance.DigitalSignedLabel = ""; // hide the default caption
            appearance.ShowContactInfo = false;
            appearance.ShowLocation = false;
            appearance.ShowReason = false;

            pkcs7.CustomAppearance = appearance;

            System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 50);
            pdfSignature.Sign(1, true, signatureRect, pkcs7);
            pdfSignature.Save(outputPdf);
        }

        Console.WriteLine("PDF signed and saved to '" + outputPdf + "'.");
    }
}