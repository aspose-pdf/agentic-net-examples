using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "encrypted.pdf";
        const string publicCertPath = "public.cer"; // public certificate from HSM

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(publicCertPath))
        {
            Console.Error.WriteLine($"Public certificate not found: {publicCertPath}");
            return;
        }

        // Load the public certificate (the private key resides on the smart card/HSM)
        X509Certificate2 publicCertificate = new X509Certificate2(publicCertPath);

        // Permissions to allow after decryption (adjust as needed)
        Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

        // Encrypt the PDF using the public certificate and AES-256 algorithm
        using (Document doc = new Document(inputPdfPath))
        {
            doc.Encrypt(permissions, CryptoAlgorithm.AESx256, new List<X509Certificate2> { publicCertificate });
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF encrypted successfully. Saved to '{outputPdfPath}'.");
    }
}