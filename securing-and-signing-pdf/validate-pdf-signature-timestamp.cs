using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // SignatureField, PKCS7, Signature, etc.

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
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Iterate over all fields and pick the signature ones
                foreach (Field f in doc.Form.Fields)
                {
                    if (f is SignatureField sigField && sigField.Signature != null)
                    {
                        // Validate the digital signature (Verify returns true if the signature is cryptographically valid)
                        bool signatureValid = sigField.Signature.Verify();

                        // Obtain the signing timestamp from the signature (Date property holds the signing time)
                        DateTime signingTime = sigField.Signature.Date;

                        // Retrieve the X.509 certificate used for signing
                        X509Certificate2 cert = null;
                        using (Stream certStream = sigField.ExtractCertificate())
                        {
                            if (certStream != null && certStream.Length > 0)
                            {
                                using (var ms = new MemoryStream())
                                {
                                    certStream.CopyTo(ms);
                                    cert = new X509Certificate2(ms.ToArray());
                                }
                            }
                        }

                        // Ensure the signing timestamp falls within the certificate's validity period
                        bool withinValidity = false;
                        if (cert != null)
                        {
                            withinValidity = signingTime >= cert.NotBefore && signingTime <= cert.NotAfter;
                        }

                        // Output results
                        Console.WriteLine($"Signature field: {sigField.PartialName}");
                        Console.WriteLine($"  Valid: {signatureValid}");
                        Console.WriteLine($"  Signing time (UTC): {signingTime:u}");
                        if (cert != null)
                        {
                            Console.WriteLine($"  Certificate valid from {cert.NotBefore:u} to {cert.NotAfter:u}");
                        }
                        else
                        {
                            Console.WriteLine("  Certificate could not be extracted.");
                        }
                        Console.WriteLine($"  Timestamp within certificate validity: {withinValidity}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
