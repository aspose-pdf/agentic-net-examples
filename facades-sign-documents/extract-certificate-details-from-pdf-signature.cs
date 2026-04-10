using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf.Facades;

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

        // Use PdfFileSignature facade to work with digital signatures
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Bind the PDF file for reading
            pdfSignature.BindPdf(inputPdf);

            // Retrieve all non‑empty signature field names (returns IList<SignatureName>)
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);

            if (signatureNames == null || signatureNames.Count == 0)
            {
                Console.WriteLine("No signatures found in the document.");
                return;
            }

            // Iterate over each signature and extract its certificate
            foreach (SignatureName sig in signatureNames)
            {
                // Try to extract the X.509 certificate associated with the signature
                if (pdfSignature.TryExtractCertificate(sig, out X509Certificate2 certificate))
                {
                    Console.WriteLine($"Signature Field: {sig.Name}");
                    Console.WriteLine($"  Issuer          : {certificate.Issuer}");
                    Console.WriteLine($"  Expiration Date: {certificate.NotAfter}");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"Signature Field: {sig.Name} – No certificate could be extracted.");
                }
            }
        }
    }
}
