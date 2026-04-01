using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);

            // Retrieve all filled signature field names
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);
            if (signatureNames == null || signatureNames.Count == 0)
            {
                Console.WriteLine("No signatures found in the PDF.");
                return;
            }

            // Process the first signature (adjust as needed for multiple signatures)
            SignatureName firstSignature = signatureNames[0];
            X509Certificate2 certificate;
            bool extracted = pdfSignature.TryExtractCertificate(firstSignature, out certificate);
            if (extracted && certificate != null)
            {
                Console.WriteLine("Signature Name: " + firstSignature.Name);
                Console.WriteLine("Certificate Serial Number: " + certificate.SerialNumber);
            }
            else
            {
                Console.WriteLine("Failed to extract certificate from signature: " + firstSignature.Name);
            }
        }
    }
}
