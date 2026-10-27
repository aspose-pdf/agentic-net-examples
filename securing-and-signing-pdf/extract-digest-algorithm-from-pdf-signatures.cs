using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve all signature fields from the document's AcroForm
                var signatureFields = doc.Form?.Fields?.OfType<SignatureField>();

                if (signatureFields == null || !signatureFields.Any())
                {
                    Console.WriteLine("No digital signatures found in the document.");
                    return;
                }

                int index = 1;
                foreach (var sigField in signatureFields)
                {
                    // The Signature property holds the actual digital signature information
                    var signature = sigField.Signature;
                    if (signature == null)
                    {
                        Console.WriteLine($"Signature #{index}: No signature data available.");
                    }
                    else
                    {
                        // Extract the signing certificate and infer the digest algorithm from its signature algorithm
                        X509Certificate2 cert = sigField.ExtractCertificateObject();
                        string digestAlgorithm = "Unknown";
                        if (cert != null && cert.SignatureAlgorithm != null)
                        {
                            // The friendly name often contains the hash algorithm, e.g., "sha256RSA"
                            string algName = cert.SignatureAlgorithm.FriendlyName?.ToUpperInvariant() ?? string.Empty;
                            if (algName.Contains("SHA256")) digestAlgorithm = "SHA256";
                            else if (algName.Contains("SHA384")) digestAlgorithm = "SHA384";
                            else if (algName.Contains("SHA512")) digestAlgorithm = "SHA512";
                            else if (algName.Contains("SHA1"))   digestAlgorithm = "SHA1";
                            else if (algName.Contains("MD5"))    digestAlgorithm = "MD5";
                        }

                        Console.WriteLine($"Signature #{index}: Digest Algorithm = {digestAlgorithm}");
                    }
                    index++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
