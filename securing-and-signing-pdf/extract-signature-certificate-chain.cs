using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        try
        {
            // Load the PDF document using the core Aspose.Pdf API (no Facades).
            using (var document = new Document(pdfPath))
            {
                bool signatureFound = false;

                // Iterate over all form fields and look for SignatureField objects.
                foreach (Field field in document.Form.Fields)
                {
                    if (field is SignatureField signatureField)
                    {
                        signatureFound = true;

                        // Extract the signing certificate from the signature field.
                        X509Certificate2 signingCert = signatureField.ExtractCertificateObject();
                        if (signingCert == null)
                        {
                            Console.WriteLine("Signature field does not contain a certificate.");
                            continue;
                        }

                        // Build the certificate chain for the extracted certificate.
                        var chain = new X509Chain();
                        chain.Build(signingCert);
                        var elements = chain.ChainElements;

                        Console.WriteLine($"Certificate chain contains {elements.Count} certificate(s):");
                        int index = 1;
                        foreach (X509ChainElement element in elements)
                        {
                            X509Certificate2 cert = element.Certificate;
                            Console.WriteLine($"Certificate {index}:");
                            Console.WriteLine($"  Subject   : {cert.Subject}");
                            Console.WriteLine($"  Issuer    : {cert.Issuer}");
                            Console.WriteLine($"  NotBefore : {cert.NotBefore}");
                            Console.WriteLine($"  NotAfter  : {cert.NotAfter}");

                            // Verify expiration.
                            string status = cert.NotAfter < DateTime.UtcNow ? "EXPIRED" : "VALID (not expired)";
                            Console.WriteLine($"  Status    : {status}\n");
                            index++;
                        }
                    }
                }

                if (!signatureFound)
                {
                    Console.WriteLine("No signature fields with certificates found in the PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
