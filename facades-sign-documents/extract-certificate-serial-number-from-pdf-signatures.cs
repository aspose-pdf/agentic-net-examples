using System;
using System.IO;
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
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(pdfPath);

            // Verify that the document contains at least one digital signature
            if (!pdfSignature.ContainsSignature())
            {
                Console.WriteLine("No digital signatures found in the PDF.");
                return;
            }

            // Retrieve all signature names (including empty fields if any)
            // GetSignatureNames returns an array of SignatureName objects, not strings.
            var signatureNames = pdfSignature.GetSignatureNames(true);

            foreach (SignatureName sigNameObj in signatureNames)
            {
                // The human‑readable name can be obtained via the Name property.
                string sigName = sigNameObj.Name;

                // Try to extract the X.509 certificate associated with the signature.
                if (pdfSignature.TryExtractCertificate(sigNameObj, out X509Certificate2 cert))
                {
                    // SerialNumber is a hexadecimal string representation.
                    string serialNumber = cert.SerialNumber;
                    Console.WriteLine($"Signature: {sigName}");
                    Console.WriteLine($"  Certificate Serial Number: {serialNumber}");
                }
                else
                {
                    Console.WriteLine($"Signature: {sigName} - No certificate could be extracted.");
                }
            }
        }
    }
}
