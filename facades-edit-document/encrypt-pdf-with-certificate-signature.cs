using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "encrypted_signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "certPassword";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the certificate using the recommended X509CertificateLoader API (avoids obsolete constructors)
        X509Certificate2 cert = X509CertificateLoader.LoadPkcs12FromFile(certificatePath, certificatePassword);
        IList<X509Certificate2> certList = new List<X509Certificate2> { cert };

        // Open the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Define the permissions that will be granted after encryption
            // Note: ModifyAnnotations flag is not available in older Aspose.Pdf versions, so it is omitted.
            Aspose.Pdf.Permissions permissions = Aspose.Pdf.Permissions.PrintDocument |
                                                Aspose.Pdf.Permissions.ExtractContent;

            // Apply certificate‑based encryption (AES‑256 is the strongest supported algorithm)
            // The overload expects: (Permissions, CryptoAlgorithm, IList<X509Certificate2>)
            pdfDocument.Encrypt(permissions, Aspose.Pdf.CryptoAlgorithm.AESx256, certList);

            // NOTE: The RequireSignature property is not present in the version of Aspose.Pdf used for this build.
            // To enforce a signature before opening, upgrade to a newer Aspose.Pdf version that provides
            // Document.RequireSignature, or implement a custom OpenAction that validates a signature.

            // Save the encrypted PDF (the document remains open until Save completes)
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF encrypted with certificate saved to '{outputPdfPath}'.");
    }
}
