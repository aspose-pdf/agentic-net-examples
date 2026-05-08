using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileSignature facade and bind the PDF file
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // Retrieve the names of all non‑empty signature fields
            var signatureNames = pdfSignature.GetSignatureNames(false); // returns SignatureName[]

            foreach (SignatureName sigName in signatureNames)
            {
                // Try to extract the X.509 certificate associated with the signature
                if (pdfSignature.TryExtractCertificate(sigName, out X509Certificate2 cert))
                {
                    Console.WriteLine($"Signature: {sigName}");
                    Console.WriteLine($"  Issuer: {cert.Issuer}");
                    Console.WriteLine($"  Expiration Date: {cert.NotAfter}");
                }
                else
                {
                    Console.WriteLine($"Signature '{sigName}' does not contain a certificate.");
                }
            }
        }
    }
}
