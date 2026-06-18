using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed_document.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the facade and bind the PDF file
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(pdfPath);

        // Retrieve all non‑empty signature names (IList<SignatureName>)
        IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);

        if (signatureNames == null || signatureNames.Count == 0)
        {
            Console.WriteLine("No signatures found in the document.");
            pdfSignature.Close();
            return;
        }

        // Iterate over each signature, extract its certificate and log the serial number
        foreach (SignatureName sig in signatureNames)
        {
            // Try to extract the X509 certificate associated with the signature
            if (pdfSignature.TryExtractCertificate(sig, out X509Certificate2 cert))
            {
                // SerialNumber is a hexadecimal string; log it for audit purposes
                Console.WriteLine($"Signature: {sig.Name}");
                Console.WriteLine($"  Certificate Subject : {cert.Subject}");
                Console.WriteLine($"  Serial Number        : {cert.SerialNumber}");
            }
            else
            {
                Console.WriteLine($"Signature: {sig.Name} – certificate could not be extracted.");
            }
        }

        // Release resources held by the facade
        pdfSignature.Close();
    }
}
