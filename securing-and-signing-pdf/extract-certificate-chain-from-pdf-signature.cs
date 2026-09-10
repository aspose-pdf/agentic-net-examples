using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";               // source PDF with digital signature
        const string outputDir = "certificates";            // folder to store DER files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all form fields; look for signature fields
            foreach (var field in doc.Form)
            {
                if (field is SignatureField sigField)
                {
                    // Try to get the X509Certificate2 object from the signature
                    X509Certificate2 cert = sigField.ExtractCertificateObject();
                    if (cert == null)
                    {
                        Console.WriteLine("No certificate found in this signature field.");
                        continue;
                    }

                    // Build the certificate chain using .NET X509Chain
                    X509Chain chain = new X509Chain();
                    chain.Build(cert);

                    // Save each certificate in the chain as a DER file
                    int index = 0;
                    foreach (X509ChainElement element in chain.ChainElements)
                    {
                        byte[] rawData = element.Certificate.RawData; // DER-encoded bytes
                        string outPath = Path.Combine(outputDir, $"cert_{index}.der");
                        File.WriteAllBytes(outPath, rawData);
                        Console.WriteLine($"Saved certificate {index} to {outPath}");
                        index++;
                    }
                }
            }
        }

        Console.WriteLine("Certificate extraction completed.");
    }
}