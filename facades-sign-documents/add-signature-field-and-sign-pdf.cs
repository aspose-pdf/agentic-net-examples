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
        const string sourcePdf = "source.pdf";          // temporary source PDF
        const string tempPdf   = "temp_with_field.pdf"; // PDF that will contain the signature field
        const string signedPdf = "signed_output.pdf";   // final signed PDF
        const string certPath  = "certificate.pfx";    // PFX certificate file
        const string certPass  = "password";           // certificate password

        // -----------------------------------------------------------------
        // Step 0: Ensure a source PDF exists.
        // -----------------------------------------------------------------
        if (!File.Exists(sourcePdf))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(sourcePdf);
            }
        }

        // -----------------------------------------------------------------
        // Step 0b: Create a self‑signed certificate if it does not exist.
        // -----------------------------------------------------------------
        if (!File.Exists(certPath))
        {
            // Generate a RSA key pair.
            using (RSA rsa = RSA.Create(2048))
            {
                var request = new CertificateRequest(
                    "cn=AsposeTest",
                    rsa,
                    HashAlgorithmName.SHA256,
                    RSASignaturePadding.Pkcs1);

                // Basic constraints – self signed.
                request.CertificateExtensions.Add(
                    new X509BasicConstraintsExtension(true, false, 0, true));
                request.CertificateExtensions.Add(
                    new X509SubjectKeyIdentifierExtension(request.PublicKey, false));

                // Create the certificate valid for 1 year.
                DateTimeOffset notBefore = DateTimeOffset.UtcNow.AddDays(-1);
                DateTimeOffset notAfter  = notBefore.AddYears(1);
                using (X509Certificate2 cert = request.CreateSelfSigned(notBefore, notAfter))
                {
                    // Export as PFX with the supplied password.
                    byte[] pfxBytes = cert.Export(X509ContentType.Pfx, certPass);
                    File.WriteAllBytes(certPath, pfxBytes);
                }
            }
        }

        // -----------------------------------------------------------------
        // Step 1: Load the PDF and add a signature field on page 1.
        // -----------------------------------------------------------------
        using (Document doc = new Document(sourcePdf))
        {
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a signature field named "Signature1" on page 1.
                // Coordinates are lower‑left (llx,lly) and upper‑right (urx,ury).
                formEditor.AddField(
                    FieldType.Signature,   // field type
                    "Signature1",          // field name
                    1,                      // page number (1‑based)
                    100f, 100f,             // llx, lly
                    250f, 150f);            // urx, ury

                // Save the document that now contains the empty signature field.
                formEditor.Save(tempPdf);
            }
        }

        // -----------------------------------------------------------------
        // Step 2: Sign the PDF using the previously created signature field.
        // -----------------------------------------------------------------
        using (PdfFileSignature pdfSigner = new PdfFileSignature())
        {
            // Bind the PDF that contains the empty signature field.
            pdfSigner.BindPdf(tempPdf);

            // Load the signing certificate.
            pdfSigner.SetCertificate(certPath, certPass);

            // Create a PKCS7 signature object.
            PKCS7 signature = new PKCS7(certPath, certPass)
            {
                Reason      = "Document approved",
                ContactInfo = "john.doe@example.com",
                Location    = "New York"
            };

            // Sign the document using the field name "Signature1".
            pdfSigner.Sign("Signature1", signature);

            // Save the signed PDF.
            pdfSigner.Save(signedPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{signedPdf}'.");
    }
}
