using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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
            using (Document document = new Document(inputPath))
            {
                // Bind the document to the signature facade
                PdfFileSignature pdfSignature = new PdfFileSignature(document);

                // Get all signature field names (including inactive ones if onlyActive = true)
                IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);

                if (signatureNames == null || signatureNames.Count == 0)
                {
                    Console.WriteLine("No signatures found in the document.");
                    return;
                }

                foreach (SignatureName sig in signatureNames)
                {
                    X509Certificate2 certificate = null;
                    bool extracted = pdfSignature.TryExtractCertificate(sig, out certificate);

                    if (extracted && certificate != null)
                    {
                        Console.WriteLine($"Signature: {sig.Name}");
                        Console.WriteLine($"  Issuer   : {certificate.Issuer}");
                        Console.WriteLine($"  Expires  : {certificate.NotAfter}");
                    }
                    else
                    {
                        Console.WriteLine($"Signature: {sig.Name} – certificate could not be extracted.");
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
