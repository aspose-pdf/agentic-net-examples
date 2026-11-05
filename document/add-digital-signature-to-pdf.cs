using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // Create a self‑signed certificate (RSA 2048‑bit, SHA‑256)
            // -----------------------------------------------------------------
            using (RSA rsa = RSA.Create(2048))
            {
                var certRequest = new CertificateRequest(
                    "cn=SelfSignedCertificate",
                    rsa,
                    HashAlgorithmName.SHA256,
                    RSASignaturePadding.Pkcs1);

                // Basic constraints: not a CA
                certRequest.CertificateExtensions.Add(
                    new X509BasicConstraintsExtension(false, false, 0, false));

                // Key usage: digital signature
                certRequest.CertificateExtensions.Add(
                    new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, false));

                // Enhanced key usage: code signing (optional)
                certRequest.CertificateExtensions.Add(
                    new X509EnhancedKeyUsageExtension(
                        new OidCollection { new Oid("1.3.6.1.5.5.7.3.3") }, false));

                // Validity period
                DateTimeOffset notBefore = DateTimeOffset.Now.AddDays(-1);
                DateTimeOffset notAfter = DateTimeOffset.Now.AddYears(1);

                // Create the self‑signed certificate
                using (var tempCert = certRequest.CreateSelfSigned(notBefore, notAfter))
                {
                    // Export as PFX to ensure the private key is included
                    byte[] pfxData = tempCert.Export(X509ContentType.Pkcs12);

                    // Write the PFX to a temporary file – Aspose.Pdf PKCS7 constructor expects a file path in this version
                    string tempPfxPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pfx");
                    File.WriteAllBytes(tempPfxPath, pfxData);

                    try
                    {
                        // -----------------------------------------------------------------
                        // Prepare the PKCS7 signature object (visual appearance is set via properties)
                        // -----------------------------------------------------------------
                        PKCS7 pkcs7 = new PKCS7(tempPfxPath, null);
                        pkcs7.Reason = "Document approved";
                        pkcs7.Location = "Office";
                        pkcs7.ContactInfo = "contact@example.com";
                        // No graphic is set – you could assign pkcs7.CustomAppearance if needed

                        // -----------------------------------------------------------------
                        // Create a signature field on the first page and sign it
                        // -----------------------------------------------------------------
                        var signatureRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
                        var signatureField = new SignatureField(pdfDoc.Pages[1], signatureRect)
                        {
                            PartialName = "Signature1"
                        };
                        // Add the field to the document's form collection (page 1)
                        pdfDoc.Form.Add(signatureField, 1);
                        // Apply the digital signature
                        signatureField.Sign(pkcs7);

                        // -----------------------------------------------------------------
                        // Save the signed PDF
                        // -----------------------------------------------------------------
                        pdfDoc.Save(outputPdfPath);
                    }
                    finally
                    {
                        // Clean up the temporary PFX file
                        if (File.Exists(tempPfxPath))
                        {
                            File.Delete(tempPfxPath);
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
