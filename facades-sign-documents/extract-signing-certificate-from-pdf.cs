using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCerPath = "LegalSignature.cer";
        const string signatureFieldName = "LegalSignature";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPdfPath);

            bool fieldFound = false;
            foreach (Field field in pdfDoc.Form.Fields)
            {
                // Look for a signature field with the specified name
                if (field is SignatureField sigField &&
                    string.Equals(sigField.PartialName, signatureFieldName, StringComparison.OrdinalIgnoreCase))
                {
                    fieldFound = true;
                    // Extract the signing certificate from the signature field
                    X509Certificate2 cert = sigField.ExtractCertificateObject();
                    if (cert == null)
                    {
                        Console.Error.WriteLine($"Signature field '{signatureFieldName}' does not contain a certificate.");
                        return;
                    }

                    // Save the certificate in DER format as a .cer file
                    byte[] certBytes = cert.Export(X509ContentType.Cert);
                    File.WriteAllBytes(outputCerPath, certBytes);
                    Console.WriteLine($"Certificate extracted and saved to '{outputCerPath}'.");
                    break;
                }
            }

            if (!fieldFound)
            {
                Console.Error.WriteLine($"Signature field '{signatureFieldName}' not found in the PDF.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
