using System;
using System.Drawing;
using System.Globalization;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF to be signed
        const string certFile   = "certificate.pfx";   // signing certificate
        const string certPass   = "password";           // certificate password
        const string appearance = "signature.png";      // optional image for visual appearance

        // Verify source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        // German signature
        SignPdf(
            inputPdf,
            $"signed_de.pdf",
            certFile,
            certPass,
            appearance,
            new CultureInfo("de-DE"),
            "Datum",               // DateSignedAtLabel
            "Digital signiert von",// DigitalSignedLabel
            "Grund",               // ReasonLabel
            "Ort",                 // LocationLabel
            "Arial",
            10,
            Aspose.Pdf.Color.Blue);

        // Spanish signature
        SignPdf(
            inputPdf,
            $"signed_es.pdf",
            certFile,
            certPass,
            appearance,
            new CultureInfo("es-ES"),
            "Fecha",               // DateSignedAtLabel
            "Firmado digitalmente por", // DigitalSignedLabel
            "Motivo",              // ReasonLabel
            "Ubicación",           // LocationLabel
            "Arial",
            10,
            Aspose.Pdf.Color.Blue);

        // Japanese signature
        SignPdf(
            inputPdf,
            $"signed_ja.pdf",
            certFile,
            certPass,
            appearance,
            new CultureInfo("ja-JP"),
            "日付",                // DateSignedAtLabel
            "デジタル署名者",      // DigitalSignedLabel
            "理由",                // ReasonLabel
            "場所",                // LocationLabel
            "MS Gothic",
            10,
            Aspose.Pdf.Color.Blue);
    }

    /// <summary>
    /// Signs a PDF using a PKCS7 signature and custom appearance localized for a specific culture.
    /// </summary>
    static void SignPdf(
        string sourcePath,
        string outputPath,
        string certPath,
        string certPassword,
        string appearanceImage,
        CultureInfo culture,
        string dateLabel,
        string signedLabel,
        string reasonLabel,
        string locationLabel,
        string fontFamily,
        double fontSize,
        Aspose.Pdf.Color textColor)
    {
        // Load the PDF into the facade
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(sourcePath);

            // Optional visual appearance (image)
            if (!string.IsNullOrEmpty(appearanceImage))
                pdfSign.SignatureAppearance = appearanceImage;

            // Prepare the PKCS7 signature object
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword);
            pkcs7.Reason   = "Document approval";
            pkcs7.ContactInfo = "support@example.com";
            pkcs7.Location = "Head Office";

            // Configure localized appearance
            SignatureCustomAppearance custom = new SignatureCustomAppearance
            {
                Culture               = culture,
                DateSignedAtLabel     = dateLabel,
                DigitalSignedLabel    = signedLabel,
                ReasonLabel           = reasonLabel,
                LocationLabel         = locationLabel,
                FontFamilyName        = fontFamily,
                FontSize              = fontSize,
                ForegroundColor       = textColor
            };
            pkcs7.CustomAppearance = custom;

            // Define the rectangle where the signature will be placed (using System.Drawing.Rectangle)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Sign page 1 (Aspose.Pdf uses 1‑based page indexing)
            pdfSign.Sign(1, true, rect, pkcs7);

            // Save the signed PDF
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}' with culture '{culture.Name}'.");
    }
}