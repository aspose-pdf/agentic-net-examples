using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF with the PdfFileSignature facade
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // Retrieve all non‑empty signature names
            var signatureNames = pdfSignature.GetSignatureNames();

            foreach (var sigName in signatureNames)
            {
                // Try to extract the X.509 certificate associated with the signature
                if (pdfSignature.TryExtractCertificate(sigName, out X509Certificate2 certificate))
                {
                    Console.WriteLine($"Signature: {sigName}");
                    Console.WriteLine($"  Issuer: {certificate.Issuer}");
                    Console.WriteLine($"  Expiration: {certificate.NotAfter}");
                }
                else
                {
                    Console.WriteLine($"Signature: {sigName} – no certificate found.");
                }
            }
        }
    }
}