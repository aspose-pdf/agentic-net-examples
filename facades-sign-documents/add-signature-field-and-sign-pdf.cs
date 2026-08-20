using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF (generated if missing)
        const string tempPdf = "temp_with_field.pdf"; // intermediate PDF with signature field
        const string signedPdf = "signed.pdf";        // final signed PDF
        const string certPath = "certificate.pfx";   // signing certificate (generated if missing)
        const string certPass = "password";          // certificate password

        // ------------------------------------------------------------
        // Ensure a source PDF exists – create a minimal one‑page PDF
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPdf);
        }

        // ------------------------------------------------------------
        // Ensure a signing certificate exists – generate a self‑signed PFX
        // ------------------------------------------------------------
        if (!File.Exists(certPath))
        {
            GenerateSelfSignedCertificate(certPath, certPass);
        }

        // ------------------------------------------------------------
        // 1. Add an empty signature field to the desired page.
        // ------------------------------------------------------------
        Document srcDoc = new Document(inputPdf);
        FormEditor formEditor = new FormEditor(srcDoc);
        // FieldType.Signature creates a signature form field.
        // Parameters: field type, field name, page number (1‑based), llx, lly, urx, ury.
        formEditor.AddField(FieldType.Signature, "Signature1", 1, 100f, 100f, 200f, 150f);
        formEditor.Save(tempPdf);

        // ------------------------------------------------------------
        // 2. Sign the PDF using the previously created signature field.
        // ------------------------------------------------------------
        PdfFileSignature pdfSigner = new PdfFileSignature();
        pdfSigner.BindPdf(tempPdf);                     // load the PDF that contains the empty field
        pdfSigner.SetCertificate(certPath, certPass);  // associate the signing certificate

        // Create a PKCS#1 signature object.
        PKCS1 signature = new PKCS1(certPath, certPass)
        {
            Reason = "Document approval",
            ContactInfo = "john.doe@example.com",
            Location = "New York"
        };

        // Sign the document using the field named "Signature1".
        pdfSigner.Sign("Signature1", signature);
        pdfSigner.Save(signedPdf);
    }

    // --------------------------------------------------------------------
    // Helper: generate a temporary self‑signed certificate and export as PFX
    // --------------------------------------------------------------------
    private static void GenerateSelfSignedCertificate(string path, string password)
    {
        using RSA rsa = RSA.Create(2048);
        var request = new CertificateRequest(
            "CN=AsposeSample",
            rsa,
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1);

        // Basic Constraints – self‑signed CA
        request.CertificateExtensions.Add(
            new X509BasicConstraintsExtension(true, false, 0, true));

        // Subject Key Identifier
        request.CertificateExtensions.Add(
            new X509SubjectKeyIdentifierExtension(request.PublicKey, false));

        // Enhanced Key Usage – digital signature
        request.CertificateExtensions.Add(
            new X509EnhancedKeyUsageExtension(
                new OidCollection { new Oid("1.3.6.1.5.5.7.3.3") }, false));

        // Validity period
        var cert = request.CreateSelfSigned(DateTimeOffset.UtcNow.AddDays(-1), DateTimeOffset.UtcNow.AddYears(1));

        // Export as PFX and write to disk
        byte[] pfxBytes = cert.Export(X509ContentType.Pfx, password);
        File.WriteAllBytes(path, pfxBytes);
    }
}
