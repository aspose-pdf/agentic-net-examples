using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "encrypted_output.pdf";
        const string publicCertPath = "public_certificate.cer"; // public part of the HSM certificate

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(publicCertPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {publicCertPath}");
            return;
        }

        // Load the public certificate (private key resides on the smart card/HSM)
        X509Certificate2 publicCertificate = new X509Certificate2(publicCertPath);
        IList<X509Certificate2> recipientCertificates = new List<X509Certificate2> { publicCertificate };

        // Define permissions for the encrypted PDF
        Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

        // Load the PDF, encrypt it with the public certificate, and save the result
        using (Document doc = new Document(inputPdfPath))
        {
            // Encrypt using certificate-based encryption (AES-256 for content protection)
            doc.Encrypt(permissions, CryptoAlgorithm.AESx256, recipientCertificates);
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF encrypted successfully and saved to '{outputPdfPath}'.");
    }
}