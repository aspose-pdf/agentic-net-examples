using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Security.Cryptography.X509Certificates;

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

        // Bind the PDF to the PdfFileSignature facade.
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPdf);

            // Retrieve all non‑empty signature field names.
            var signatureNames = pdfSign.GetSignatureNames(true);
            if (signatureNames == null || signatureNames.Count == 0)
            {
                Console.WriteLine("No signatures found in the document.");
                return;
            }

            // Iterate over each signature and extract its certificate.
            foreach (SignatureName sig in signatureNames)
            {
                // Try to get the X.509 certificate associated with the signature.
                if (pdfSign.TryExtractCertificate(sig, out X509Certificate2 cert) && cert != null)
                {
                    Console.WriteLine($"Signature: {sig.Name}");
                    Console.WriteLine($"  Issuer: {cert.Issuer}");
                    Console.WriteLine($"  Expiration Date: {cert.NotAfter}");
                }
                else
                {
                    Console.WriteLine($"Signature: {sig.Name} – certificate could not be extracted.");
                }
            }
        }
    }
}
