using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "encrypted_signed.pdf";
        const string attachmentPath = "sample.txt";
        const string certPath = "signcert.pfx";
        const string certPassword = "password";

        // Verify required files exist
        if (!File.Exists(attachmentPath) || !File.Exists(certPath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the certificate used for both encryption and signing
        X509Certificate2 cert = new X509Certificate2(certPath, certPassword);
        IList<X509Certificate2> certList = new List<X509Certificate2> { cert };

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to host the signature field
            Page page = doc.Pages.Add();

            // Attach a file to the PDF using FileSpecification
            FileSpecification fileSpec = new FileSpecification(attachmentPath);
            doc.EmbeddedFiles.Add(fileSpec);

            // Encrypt the document for the recipient certificate using AES‑256
            doc.Encrypt(Permissions.PrintDocument | Permissions.ExtractContent,
                        CryptoAlgorithm.AESx256,
                        certList);

            // Define a rectangle for the signature field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(page, sigRect);
            doc.Form.Add(sigField);

            // Create a PKCS#1 signature object using the same certificate
            PKCS1 pkcs1 = new PKCS1(certPath, certPassword);

            // Apply the digital signature to the signature field
            sigField.Sign(pkcs1);

            // Save the encrypted and signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created, encrypted and signed: {outputPath}");
    }
}
