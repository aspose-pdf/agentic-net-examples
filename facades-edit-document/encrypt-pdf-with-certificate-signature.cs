using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string encryptedPdf = "encrypted.pdf";    // intermediate encrypted file
        const string signedPdf   = "signed.pdf";        // final output
        const string certPath    = "certificate.pfx";   // certificate file
        const string certPassword = "certPassword";     // certificate password

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // ---------- Load and encrypt the PDF (password‑based) ----------
        // Encryption uses CryptoAlgorithm (AES‑256) as required by the encryption rules.
        using (Document doc = new Document(inputPdf))
        {
            // Define permissions – allow printing and content extraction only.
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt with user and owner passwords.
            string userPassword  = "user123";
            string ownerPassword = "owner123";

            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPdf); // Save encrypted PDF
        }

        // ---------- Sign the encrypted PDF ----------
        // The PdfFileSignature facade works on the encrypted file.
        PdfFileSignature pdfSigner = new PdfFileSignature();
        pdfSigner.BindPdf(encryptedPdf);

        // Set the certificate used for signing.
        pdfSigner.SetCertificate(certPath, certPassword);

        // Optional: set a visual appearance for the signature (image file).
        // pdfSigner.SignatureAppearance = "signatureImage.jpg";

        // Define signature properties.
        string reason   = "Document certification";
        string contact  = "contact@example.com";
        string location = "New York";

        // Visible signature rectangle (System.Drawing.Rectangle is required by the API).
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Use PKCS#1 signature object.
        PKCS1 pkcs1Signature = new PKCS1(certPath, certPassword);
        pkcs1Signature.Reason   = reason;
        pkcs1Signature.ContactInfo = contact;
        pkcs1Signature.Location = location;

        // Sign on page 1 (pages are 1‑based). The signature will be visible.
        pdfSigner.Sign(1, reason, contact, location, true, rect, pkcs1Signature);

        // Save the signed PDF.
        pdfSigner.Save(signedPdf);

        // Clean up intermediate encrypted file (optional).
        try { File.Delete(encryptedPdf); } catch { }

        Console.WriteLine($"PDF has been encrypted and signed. Output: {signedPdf}");
    }
}