using System;
using System.IO;
using Aspose.Pdf.Facades;
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

        // Initialize the facade and bind the PDF file
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // Verify that the document contains at least one signature
            if (!pdfSignature.ContainsSignature())
            {
                Console.WriteLine("The PDF does not contain any digital signatures.");
                return;
            }

            // Retrieve all non‑empty signature names (returns SignatureName objects)
            var signatureNames = pdfSignature.GetSignatureNames(true);

            foreach (SignatureName sigName in signatureNames)
            {
                // Try to extract the X.509 certificate associated with the signature
                if (pdfSignature.TryExtractCertificate(sigName, out X509Certificate2 cert))
                {
                    // SerialNumber is a hexadecimal string; log it for audit purposes
                    Console.WriteLine($"Signature: {sigName.Name}, Serial Number: {cert.SerialNumber}");
                }
                else
                {
                    Console.WriteLine($"Signature: {sigName.Name}, certificate could not be extracted.");
                }
            }
        }
    }
}
