using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";

        // Verify the PDF file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the signature handler (PdfFileSignature is not IDisposable)
        PdfFileSignature signatureHandler = new PdfFileSignature();
        signatureHandler.BindPdf(inputPath);

        // Retrieve all signature names present in the document
        IList<SignatureName> signatureNames = signatureHandler.GetSignatureNames(true);

        if (signatureNames == null || signatureNames.Count == 0)
        {
            Console.WriteLine("No digital signatures found in the PDF.");
            return;
        }

        // Iterate over each signature and extract the certificate's serial number
        foreach (SignatureName sigName in signatureNames)
        {
            // Extract the certificate associated with the signature
            using (Stream certStream = signatureHandler.ExtractCertificate(sigName))
            {
                if (certStream != null && certStream.Length > 0)
                {
                    // Load the certificate into X509Certificate2 to read the serial number
                    byte[] certBytes;
                    using (var ms = new MemoryStream())
                    {
                        certStream.CopyTo(ms);
                        certBytes = ms.ToArray();
                    }

                    var cert = new X509Certificate2(certBytes);
                    string serialNumber = cert.SerialNumber;

                    if (!string.IsNullOrEmpty(serialNumber))
                    {
                        Console.WriteLine($"Signature '{sigName}' certificate serial number: {serialNumber}");
                    }
                    else
                    {
                        Console.WriteLine($"Signature '{sigName}' does not contain a certificate serial number.");
                    }
                }
                else
                {
                    Console.WriteLine($"Signature '{sigName}' does not contain a certificate.");
                }
            }
        }
    }
}
