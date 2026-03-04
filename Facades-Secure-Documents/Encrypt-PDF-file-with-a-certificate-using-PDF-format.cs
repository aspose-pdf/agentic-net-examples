using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facade namespace is included as requested
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the public certificate (X.509)
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "encrypted.pdf";
        const string certPath      = "publicCert.cer";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the public certificate that will be used for encryption
        X509Certificate2 publicCert = new X509Certificate2(certPath);

        // Load the PDF document (using the core Document API)
        Document pdfDoc = new Document(inputPdfPath);

        // Prepare a list of recipient certificates – one certificate per recipient
        IList<X509Certificate2> recipientCertificates = new List<X509Certificate2> { publicCert };

        // Define the permissions that should be allowed after encryption
        // For example, allow printing and content extraction
        Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

        // Encrypt the document with the certificate.
        // The CryptoAlgorithm parameter selects the symmetric algorithm (AESx256 is the strongest supported).
        pdfDoc.Encrypt(permissions, CryptoAlgorithm.AESx256, recipientCertificates);

        // Save the encrypted PDF. The Facade namespace is referenced, but the actual save
        // operation uses the core Document API as required by the lifecycle rules.
        pdfDoc.Save(outputPdfPath);

        Console.WriteLine($"PDF encrypted with certificate and saved to '{outputPdfPath}'.");
    }
}