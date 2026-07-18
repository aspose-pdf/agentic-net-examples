using System;
using System.Drawing;
using System.Globalization;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // for SignatureCustomAppearance

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPwd = "password";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the signature facade
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPdf);
            pdfSign.SetCertificate(certPath, certPwd);

            // Define a System.Drawing.Rectangle for the visible signature (same for all pages)
            // Aspose.Pdf.Rectangle uses (llx, lly, urx, ury). Convert to (x, y, width, height).
            System.Drawing.Rectangle sigRect = new System.Drawing.Rectangle(100, 100, 100, 50);

            // ---------- German signature (page 1) ----------
            PKCS7 germanSig = new PKCS7(certPath, certPwd);
            germanSig.CustomAppearance = new SignatureCustomAppearance { Culture = new CultureInfo("de-DE") };
            pdfSign.Sign(
                1,                     // page number (1‑based)
                "Genehmigt",          // reason
                "kontakt@example.de", // contact info
                "Berlin, Deutschland",// location
                true,                  // visible signature flag
                sigRect,               // signature rectangle
                germanSig);            // PKCS7 object with custom appearance

            // ---------- Spanish signature (page 2) ----------
            PKCS7 spanishSig = new PKCS7(certPath, certPwd);
            spanishSig.CustomAppearance = new SignatureCustomAppearance { Culture = new CultureInfo("es-ES") };
            pdfSign.Sign(
                2,
                "Aprobado",
                "contacto@example.es",
                "Madrid, España",
                true,
                sigRect,
                spanishSig);

            // ---------- Japanese signature (page 3) ----------
            PKCS7 japaneseSig = new PKCS7(certPath, certPwd);
            japaneseSig.CustomAppearance = new SignatureCustomAppearance { Culture = new CultureInfo("ja-JP") };
            pdfSign.Sign(
                3,
                "承認済み",
                "contact@example.jp",
                "東京, 日本",
                true,
                sigRect,
                japaneseSig);

            // Save the signed PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
