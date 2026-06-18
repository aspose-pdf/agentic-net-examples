using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string outputPdf = "encrypted_signed.pdf";
        const string attachmentPath = "sample.txt";          // file to attach
        const string recipientCertPath = "recipient.cer";    // recipient public cert
        const string signerPfxPath = "signer.pfx";           // signer private cert
        const string signerPfxPassword = "pfxPassword";

        // Ensure required files exist
        if (!File.Exists(attachmentPath) ||
            !File.Exists(recipientCertPath) ||
            !File.Exists(signerPfxPath))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Load recipient public certificate for encryption (use the non‑obsolete loader)
        // X509CertificateLoader.LoadCertificate expects the certificate bytes, not a file path.
        byte[] recipientCertBytes = File.ReadAllBytes(recipientCertPath);
        X509Certificate2 recipientCert = X509CertificateLoader.LoadCertificate(recipientCertBytes);
        IList<X509Certificate2> recipientCerts = new List<X509Certificate2> { recipientCert };

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required for signature field)
            Page page = doc.Pages.Add();

            // Embed a file into the PDF (use EmbeddedFiles collection)
            var fileSpec = new FileSpecification(attachmentPath, Path.GetFileName(attachmentPath));
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));
            doc.EmbeddedFiles.Add(fileSpec);

            // Encrypt the document for the recipient using AES‑256 (allow printing only)
            Permissions perms = Permissions.PrintDocument;
            doc.Encrypt(perms, CryptoAlgorithm.AESx256, recipientCerts);

            // Create a signature field on the first page
            var sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            var sigField = new SignatureField(page, sigRect);
            doc.Form.Add(sigField);

            // Prepare the signature (PKCS#7 detached) using the signer's PFX
            var signature = new PKCS7Detached(signerPfxPath, signerPfxPassword);
            // Sign the document via the signature field
            sigField.Sign(signature);

            // Save the encrypted and signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created, encrypted with attachment, and signed: {outputPdf}");
    }
}
