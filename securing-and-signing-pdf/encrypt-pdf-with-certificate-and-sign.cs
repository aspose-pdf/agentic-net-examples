using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the certificate (PFX) and the output PDF
        const string certPath = "certificate.pfx";
        const string certPassword = "password";
        const string outputPdf = "encrypted_signed.pdf";

        // Verify that the certificate file exists
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the certificate (contains both public and private keys)
        X509Certificate2 certificate = new X509Certificate2(certPath, certPassword,
            X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page and some sample text
            Page page = doc.Pages.Add();
            TextFragment tf = new TextFragment("This PDF is encrypted with a certificate and digitally signed.");
            tf.Position = new Position(100, 700);
            page.Paragraphs.Add(tf);

            // ----- Add a signature field -----
            // Define the rectangle where the visible signature will appear
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(page, sigRect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField);

            // ----- Encrypt the document with the certificate -----
            // Use AES-256 encryption and allow printing & content extraction
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            IList<X509Certificate2> publicCerts = new List<X509Certificate2> { certificate };
            doc.Encrypt(perms, CryptoAlgorithm.AESx256, publicCerts);

            // ----- Sign the PDF using the same certificate -----
            // Create a PKCS#1 signature object from the certificate file
            PKCS1 pkcs1Signature = new PKCS1(certPath, certPassword)
            {
                Reason = "Document approval",
                ContactInfo = "contact@example.com",
                Location = "Office"
            };
            // Sign the previously added signature field
            sigField.Sign(pkcs1Signature);

            // Save the encrypted and signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created, encrypted, and signed: {outputPdf}");
    }
}