using System;
using System.IO;
using System.Globalization;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "signed_output.pdf";
        string certificatePath = "certificate.pfx";
        string certificatePassword = "password";

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

        // Bind the source PDF to the signature facade
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPath);

        // Create a PKCS7 signature object with the certificate
        PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword);
        pkcs7.Reason = "Document approuvé";
        pkcs7.ContactInfo = "contact@example.com";
        pkcs7.Location = "Paris";

        // Customize the signature appearance labels to French
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
        customAppearance.Culture = new CultureInfo("fr-FR");
        customAppearance.DigitalSignedLabel = "Signé numériquement";
        customAppearance.DateSignedAtLabel = "Date de signature";
        customAppearance.ReasonLabel = "Raison";
        customAppearance.LocationLabel = "Lieu";
        customAppearance.FontFamilyName = "Helvetica";
        customAppearance.FontSize = 12;
        pkcs7.CustomAppearance = customAppearance;

        // Define the rectangle where the visible signature will appear
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Sign page 1 with the customized appearance
        pdfSignature.Sign(1, true, rect, pkcs7);

        // Save the signed PDF
        pdfSignature.Save(outputPath);

        Console.WriteLine("Signed PDF saved to '" + outputPath + "'.");
    }
}
