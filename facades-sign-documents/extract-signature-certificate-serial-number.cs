using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the PdfFileSignature facade and bind the PDF file
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(pdfPath);

            // Retrieve all signature names (including empty fields if desired)
            var signatureNames = pdfSignature.GetSignatureNames(true); // returns IList<SignatureName>

            foreach (var sigInfo in signatureNames)
            {
                // Attempt to extract the X.509 certificate for the current signature
                if (pdfSignature.TryExtractCertificate(sigInfo, out X509Certificate2 certificate))
                {
                    // Log the serial number (hex string) for audit purposes
                    Console.WriteLine($"Signature '{sigInfo.Name}' – Serial Number: {certificate.SerialNumber}");
                }
                else
                {
                    Console.WriteLine($"Signature '{sigInfo.Name}' – No certificate found.");
                }
            }
        }
    }
}
