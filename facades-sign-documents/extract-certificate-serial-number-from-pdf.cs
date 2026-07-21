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

        try
        {
            // Initialize the facade and bind the PDF file
            using (PdfFileSignature pdfSignature = new PdfFileSignature())
            {
                pdfSignature.BindPdf(inputPdf);

                // Verify that the document contains at least one digital signature
                if (!pdfSignature.ContainsSignature())
                {
                    Console.WriteLine("No digital signatures found in the PDF.");
                    return;
                }

                // Retrieve all signature names (non‑empty signatures)
                var signatureNames = pdfSignature.GetSignatureNames(true); // IList<SignatureName>

                foreach (var sigInfo in signatureNames)
                {
                    // The SignatureName object contains the actual name in its Name property
                    string sigName = sigInfo.Name;

                    // Try to extract the X.509 certificate associated with the signature
                    if (pdfSignature.TryExtractCertificate(sigInfo, out X509Certificate2 cert) && cert != null)
                    {
                        // SerialNumber is a hexadecimal string; log it for audit purposes
                        Console.WriteLine($"Signature: {sigName}");
                        Console.WriteLine($"  Certificate Serial Number: {cert.SerialNumber}");
                    }
                    else
                    {
                        Console.WriteLine($"Signature: {sigName} – certificate could not be extracted.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
