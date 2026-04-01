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
        const string outputPdf = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";
        const int pageNumber = 1;
        System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 50);

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

        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPdf);

        PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword);
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
        customAppearance.DigitalSignedLabel = ""; // hide default caption
        pkcs7.CustomAppearance = customAppearance;

        pdfSignature.Sign(pageNumber, true, signatureRect, pkcs7);
        pdfSignature.Save(outputPdf);
        pdfSignature.Close();

        Console.WriteLine("Signed PDF saved to '" + outputPdf + "'.");
    }
}