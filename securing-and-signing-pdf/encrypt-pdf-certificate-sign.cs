using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPdfPath = "encrypted_signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "pfxPassword";

        // Verify that the certificate file exists before attempting to load it.
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the certificate using the byte[] overload of X509CertificateLoader.
        byte[] certBytes = File.ReadAllBytes(certificatePath);
        X509Certificate2 cert = X509CertificateLoader.LoadPkcs12(certBytes, certificatePassword);

        // Create a new PDF document.
        using (Document doc = new Document())
        {
            // Add a page.
            Page page = doc.Pages.Add();

            // Add some sample text.
            TextFragment tf = new TextFragment("This PDF is encrypted with a certificate and digitally signed.");
            tf.Position = new Position(100, 700);
            page.Paragraphs.Add(tf);

            // -------------------------
            // Encrypt the document using the certificate's public key.
            // Permissions: allow printing only.
            Permissions perms = Permissions.PrintDocument;
            CryptoAlgorithm algo = CryptoAlgorithm.AESx256;
            IList<X509Certificate2> certList = new List<X509Certificate2> { cert };
            doc.Encrypt(perms, algo, certList);

            // -------------------------
            // Add a signature field to the first page.
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField signatureField = new SignatureField(page, sigRect);
            doc.Form.Add(signatureField);

            // Create a PKCS#7 signature using the same certificate (private key).
            // PKCS7 constructor expects the certificate file path, not the raw bytes.
            PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword);
            signatureField.Sign(pkcs7Signature);

            // Save the encrypted and signed PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF created, encrypted with certificate, and signed. Saved to '{outputPdfPath}'.");
    }
}
