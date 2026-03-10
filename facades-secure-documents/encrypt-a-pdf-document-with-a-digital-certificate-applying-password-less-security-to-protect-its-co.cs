using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "encrypted_output.pdf";
        const string certificatePath = "recipient.cer"; // public certificate file (DER or PEM)

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the recipient's public certificate. The loader expects the raw certificate bytes.
        byte[] certBytes = File.ReadAllBytes(certificatePath);
        X509Certificate2 recipientCert = X509CertificateLoader.LoadCertificate(certBytes);

        // Load the PDF document, encrypt it with the certificate (no user/owner passwords),
        // then save the encrypted file.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Encrypt using the public certificate. Permissions can be adjusted as needed.
            // The older "ModifyContents" flag is not available in the current library version,
            // so we only apply the PrintDocument permission here (add others that exist if needed).
            pdfDoc.Encrypt(
                Permissions.PrintDocument,
                CryptoAlgorithm.AESx256,
                new List<X509Certificate2> { recipientCert });

            // Save the encrypted PDF.
            pdfDoc.Save(outputPdfPath);
        }

        // OPTIONAL: Demonstrate usage of the Facades API (PdfFileSecurity) to set
        // password‑less privileges on the already encrypted file. This does not add
        // passwords; it simply ensures the file has a security setting without
        // user/owner passwords.
        PdfFileSecurity fileSecurity = new PdfFileSecurity();
        fileSecurity.BindPdf(outputPdfPath);
        // Set privilege without providing any passwords (owner password will be random).
        fileSecurity.SetPrivilege(DocumentPrivilege.Print);
        // Save the final file (overwrites the same file).
        fileSecurity.Save(outputPdfPath);

        Console.WriteLine($"PDF encrypted with certificate and password‑less security saved to '{outputPdfPath}'.");
    }
}
