using System;
using System.IO;
using System.Globalization;
using System.Drawing;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Create a PKCS7 signature object and set common metadata
        PKCS7 pkcs7Signature = new PKCS7(certPath, certPassword);
        pkcs7Signature.Reason = "Agreement";
        // The correct property name for contact information is ContactInfo, not Contact
        pkcs7Signature.ContactInfo = "john.doe@example.com";
        pkcs7Signature.Location = "Berlin";

        // German appearance settings
        SignatureCustomAppearance germanAppearance = new SignatureCustomAppearance();
        germanAppearance.DigitalSignedLabel = "Digital signiert von";
        germanAppearance.DateSignedAtLabel = "Datum:";
        germanAppearance.ReasonLabel = "Grund:";
        germanAppearance.LocationLabel = "Ort:";
        germanAppearance.FontFamilyName = "Helvetica";
        germanAppearance.FontSize = 10;
        germanAppearance.Culture = new CultureInfo("de-DE");
        germanAppearance.DateTimeFormat = "dd.MM.yyyy HH:mm";

        // Spanish appearance settings
        SignatureCustomAppearance spanishAppearance = new SignatureCustomAppearance();
        spanishAppearance.DigitalSignedLabel = "Firmado digitalmente por";
        spanishAppearance.DateSignedAtLabel = "Fecha:";
        spanishAppearance.ReasonLabel = "Razón:";
        spanishAppearance.LocationLabel = "Ubicación:";
        spanishAppearance.FontFamilyName = "Helvetica";
        spanishAppearance.FontSize = 10;
        spanishAppearance.Culture = new CultureInfo("es-ES");
        spanishAppearance.DateTimeFormat = "dd/MM/yyyy HH:mm";

        // Japanese appearance settings
        SignatureCustomAppearance japaneseAppearance = new SignatureCustomAppearance();
        japaneseAppearance.DigitalSignedLabel = "デジタル署名者";
        japaneseAppearance.DateSignedAtLabel = "日付:";
        japaneseAppearance.ReasonLabel = "理由:";
        japaneseAppearance.LocationLabel = "場所:";
        japaneseAppearance.FontFamilyName = "Helvetica";
        japaneseAppearance.FontSize = 10;
        japaneseAppearance.Culture = new CultureInfo("ja-JP");
        japaneseAppearance.DateTimeFormat = "yyyy/MM/dd HH:mm";

        // Initialize PdfFileSignature and bind the source PDF
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPath);

        // Sign page 1 with German appearance
        pkcs7Signature.CustomAppearance = germanAppearance;
        Rectangle rect1 = new Rectangle(100, 100, 200, 100);
        pdfSignature.Sign(1, true, rect1, pkcs7Signature);

        // Sign page 2 with Spanish appearance
        pkcs7Signature.CustomAppearance = spanishAppearance;
        Rectangle rect2 = new Rectangle(100, 200, 200, 100);
        pdfSignature.Sign(2, true, rect2, pkcs7Signature);

        // Sign page 3 with Japanese appearance
        pkcs7Signature.CustomAppearance = japaneseAppearance;
        Rectangle rect3 = new Rectangle(100, 300, 200, 100);
        pdfSignature.Sign(3, true, rect3, pkcs7Signature);

        // Save the signed document
        pdfSignature.Save(outputPath);
        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
