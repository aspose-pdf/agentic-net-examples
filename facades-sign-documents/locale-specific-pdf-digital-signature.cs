using System;
using System.Globalization;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class LocaleSpecificSignatureDemo
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf = "input.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // German signature
        SignPdfWithLocale(
            inputPdf,
            "signed_de.pdf",
            certPath,
            certPassword,
            new CultureInfo("de-DE"),
            "Digital signiert",
            "Datum:",
            "Grund:",
            "Ort:",
            "Helvetica",
            12);

        // Spanish signature
        SignPdfWithLocale(
            inputPdf,
            "signed_es.pdf",
            certPath,
            certPassword,
            new CultureInfo("es-ES"),
            "Firmado digitalmente",
            "Fecha:",
            "Razón:",
            "Ubicación:",
            "Helvetica",
            12);

        // Japanese signature
        SignPdfWithLocale(
            inputPdf,
            "signed_ja.pdf",
            certPath,
            certPassword,
            new CultureInfo("ja-JP"),
            "デジタル署名",
            "日付:",
            "理由:",
            "場所:",
            "Helvetica",
            12);
    }

    static void SignPdfWithLocale(
        string sourcePath,
        string outputPath,
        string certPath,
        string certPassword,
        CultureInfo culture,
        string digitalSignedLabel,
        string dateSignedAtLabel,
        string reasonLabel,
        string locationLabel,
        string fontFamily,
        double fontSize)
    {
        // Create the signature object (PKCS#7)
        Aspose.Pdf.Forms.PKCS7 pkcs7 = new Aspose.Pdf.Forms.PKCS7(certPath, certPassword);
        pkcs7.Reason = "Document approval";
        pkcs7.ContactInfo = "contact@example.com";
        pkcs7.Location = "Head Office";

        // Configure custom appearance with locale‑specific labels
        Aspose.Pdf.Forms.SignatureCustomAppearance customAppearance = new Aspose.Pdf.Forms.SignatureCustomAppearance
        {
            Culture = culture,
            DigitalSignedLabel = digitalSignedLabel,
            DateSignedAtLabel = dateSignedAtLabel,
            ReasonLabel = reasonLabel,
            LocationLabel = locationLabel,
            FontFamilyName = fontFamily,
            FontSize = fontSize
        };
        pkcs7.CustomAppearance = customAppearance;

        // Define the visible rectangle for the signature (System.Drawing.Rectangle)
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 50);

        // Use PdfFileSignature facade to apply the signature
        using (PdfFileSignature signer = new PdfFileSignature())
        {
            signer.BindPdf(sourcePath);
            // The signature appearance image can be set here if desired:
            // signer.SignatureAppearance = "signatureImage.jpg";

            // Sign the first page (page numbers are 1‑based)
            signer.Sign(page: 1, visible: true, annotRect: rect, sig: pkcs7);
            signer.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved: {outputPath}");
    }
}