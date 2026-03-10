using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output file paths (adjust as needed)
        const string inputPdf = "input.pdf";
        const string outputPdfPassword = "encrypted_password.pdf";
        const string outputPdfCert = "encrypted_certificate.pdf";

        // -----------------------------------------------------------------
        // Ensure a source PDF exists – create a simple one if it does not.
        // -----------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            var placeholder = new Document();
            placeholder.Pages.Add(); // add a blank page
            placeholder.Save(inputPdf);
            Console.WriteLine($"Placeholder PDF created at '{inputPdf}'.");
        }

        // -----------------------------------------------------------------
        // 1. Password‑based encryption using PdfFileSecurity (Facades API)
        // -----------------------------------------------------------------
        var pdfSecurity = new PdfFileSecurity();                     // create facade
        pdfSecurity.BindPdf(inputPdf);                               // load source PDF
        // Example: encrypt with user password "user123", owner password "owner123",
        // allow printing only, and use 256‑bit AES encryption.
        pdfSecurity.EncryptFile(
            userPassword: "user123",
            ownerPassword: "owner123",
            privilege: DocumentPrivilege.Print,
            keySize: KeySize.x256,
            cipher: Algorithm.AES);
        pdfSecurity.Save(outputPdfPassword);                         // write encrypted PDF

        // -----------------------------------------------------------------
        // 2. Password‑based encryption using the core Document API
        // -----------------------------------------------------------------
        using (var doc = new Document(inputPdf))
        {
            // Define permissions: allow printing and content extraction
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Choose a strong algorithm – AES‑256 is recommended.
            doc.Encrypt(
                userPassword: "user123",
                ownerPassword: "owner123",
                permissions: perms,
                cryptoAlgorithm: CryptoAlgorithm.AESx256);

            doc.Save(outputPdfPassword); // overwrite or use a different file name
        }

        // -----------------------------------------------------------------
        // 3. Certificate‑based encryption (public‑key encryption)
        // -----------------------------------------------------------------
        // Prepare a list with the public certificates of the intended recipients.
        IList<X509Certificate2> recipientCertificates = new List<X509Certificate2>();
        // Example placeholder – load a certificate from a file if you have one.
        // var cert = new X509Certificate2("recipient.cer");
        // recipientCertificates.Add(cert);

        // Load the PDF and encrypt it with the certificate list.
        using (var doc = new Document(inputPdf))
        {
            // Permissions can be set as needed.
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt for the recipients; if the list is empty the call will succeed
            // but no public‑key encryption will be applied (useful for demo purposes).
            doc.Encrypt(
                permissions: perms,
                cryptoAlgorithm: CryptoAlgorithm.AESx256,
                publicCertificates: recipientCertificates);

            doc.Save(outputPdfCert);
        }

        // -----------------------------------------------------------------
        // 4. Decryption examples (for completeness)
        // -----------------------------------------------------------------
        // a) Decrypt using PdfFileSecurity (owner password required)
        var decryptSecurity = new PdfFileSecurity();
        decryptSecurity.BindPdf(outputPdfPassword);
        decryptSecurity.DecryptFile(ownerPassword: "owner123");
        decryptSecurity.Save("decrypted_password.pdf");

        // b) Decrypt using Document.Decrypt (open with owner password first)
        using (var encryptedDoc = new Document(outputPdfPassword, "owner123"))
        {
            encryptedDoc.Decrypt();               // removes all encryption
            encryptedDoc.Save("decrypted_password_doc.pdf");
        }

        // c) Decrypt a certificate‑encrypted PDF (provide the private key via
        //    CertificateEncryptionOptions). This requires the private key
        //    associated with one of the public certificates used during encryption.
        //    Example placeholder – replace with actual certificate and password.
        // X509Certificate2 privateCert = new X509Certificate2("mycert.pfx", "certPassword");
        // var certOptions = new Aspose.Pdf.Security.CertificateEncryptionOptions(privateCert, StoreName.My, StoreLocation.CurrentUser);
        // using (var certDoc = new Document(outputPdfCert, certOptions))
        // {
        //     certDoc.Decrypt();
        //     certDoc.Save("decrypted_certificate.pdf");
        // }

        Console.WriteLine("Encryption and decryption examples completed.");
    }
}
