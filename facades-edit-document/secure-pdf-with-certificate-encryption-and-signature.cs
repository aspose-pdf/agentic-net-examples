using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // for PKCS1

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "secured_signed.pdf";
        const string certCer = "recipient.cer";      // public certificate for encryption
        const string signPfx = "signer.pfx";        // private certificate for signing
        const string pfxPassword = "pfxPassword";

        if (!File.Exists(inputPdf) ||
            !File.Exists(certCer) ||
            !File.Exists(signPfx))
        {
            Console.Error.WriteLine("Required file(s) not found.");
            return;
        }

        // Load the public certificate used for encryption (use the new loader API)
        X509Certificate2 publicCert = X509CertificateLoader.LoadCertificateFromFile(certCer);

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // ---------- Digital signature (certification) ----------
            PdfFileSignature pdfSignature = new PdfFileSignature();
            pdfSignature.BindPdf(doc);

            // PKCS#1 certificate for signing
            PKCS1 pkcs1 = new PKCS1(signPfx, pfxPassword);

            // Visible rectangle for the signature (System.Drawing.Rectangle)
            System.Drawing.Rectangle sigRect = new System.Drawing.Rectangle(100, 100, 200, 50);

            // Apply a certification signature on page 1 using the correct overload (positional arguments)
            pdfSignature.Sign(
                1,                                 // page number (1‑based)
                "Document certification",        // reason
                "contact@example.com",            // contact info
                "Head Office",                    // location
                true,                              // visible signature flag
                sigRect,                           // signature rectangle
                pkcs1);                            // signing certificate

            // ---------- Certificate‑based encryption ----------
            doc.Encrypt(
                permissions: Permissions.PrintDocument,
                cryptoAlgorithm: CryptoAlgorithm.AESx256,
                publicCertificates: new List<X509Certificate2> { publicCert });

            // Save the secured and signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF has been signed and encrypted: '{outputPdf}'.");
    }
}
