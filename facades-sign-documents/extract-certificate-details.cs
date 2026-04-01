using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";

        // Verify that the PDF file exists before attempting to bind it.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: The file '{Path.GetFullPath(inputPath)}' was not found.");
            return;
        }

        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Bind the existing PDF document.
            pdfSignature.BindPdf(inputPath);

            // GetSignatureNames returns IList<SignatureName>, not List<string>
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);
            if (signatureNames == null || signatureNames.Count == 0)
            {
                Console.WriteLine("No signatures found in the document.");
                return;
            }

            foreach (SignatureName sig in signatureNames)
            {
                X509Certificate2 certificate;
                // TryExtractCertificate expects a SignatureName instance
                bool extracted = pdfSignature.TryExtractCertificate(sig, out certificate);
                if (extracted && certificate != null)
                {
                    Console.WriteLine($"Signature: {sig.Name}");
                    Console.WriteLine($"  Issuer: {certificate.Issuer}");
                    Console.WriteLine($"  Expiration: {certificate.NotAfter}");
                }
                else
                {
                    Console.WriteLine($"Signature: {sig.Name} – certificate could not be extracted.");
                }
            }
        }
    }
}
