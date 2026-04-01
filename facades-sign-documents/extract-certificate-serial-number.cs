using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "signed.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdfPath);
            return;
        }

        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdfPath);

            // GetSignatureNames returns IList<SignatureName>, not a string array.
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(false);
            if (signatureNames == null || signatureNames.Count == 0)
            {
                Console.WriteLine("No signatures found in the PDF.");
                return;
            }

            foreach (SignatureName sigName in signatureNames)
            {
                // Try to extract the certificate associated with this signature.
                X509Certificate2 certificate;
                bool extracted = pdfSignature.TryExtractCertificate(sigName, out certificate);
                if (extracted && certificate != null)
                {
                    string serialNumber = certificate.SerialNumber;
                    Console.WriteLine($"Signature '{sigName.Name}' serial number: {serialNumber}");
                }
                else
                {
                    Console.WriteLine($"Certificate not found for signature '{sigName.Name}'.");
                }
            }
        }
    }
}
