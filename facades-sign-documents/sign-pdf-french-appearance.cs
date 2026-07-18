using System;
using System.IO;
using System.Globalization;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_fr.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";
        const string appearanceImage = "signature.png";

        if (!File.Exists(inputPdf) || !File.Exists(certPath))
        {
            Console.Error.WriteLine("Missing input PDF or certificate file.");
            return;
        }

        // Bind the PDF and configure signature appearance
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPdf);
            pdfSign.SignatureAppearance = appearanceImage;
            pdfSign.SetCertificate(certPath, certPassword);

            // Create a PKCS1 signature object
            PKCS1 signature = new PKCS1(certPath, certPassword);

            // Set custom appearance locale to French
            SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
            customAppearance.Culture = new CultureInfo("fr-FR");
            signature.CustomAppearance = customAppearance;

            // Define the signature rectangle (x, y, width, height)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Sign on page 1, make it visible, using the custom appearance
            pdfSign.Sign(1, true, rect, signature);

            // Save the signed PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}