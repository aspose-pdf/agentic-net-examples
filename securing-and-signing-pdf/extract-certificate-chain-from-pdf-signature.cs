using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExtractCertificateChain
{
    static void Main()
    {
        // Path to the signed PDF document
        const string inputPdf = "signed.pdf";

        // Directory where extracted DER files will be saved
        const string outputDir = "ExtractedCertificates";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify that the input PDF actually exists before trying to load it.
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Error: The file '{inputPdf}' was not found. Please provide a valid signed PDF file and retry.");
            return; // Gracefully exit the program.
        }

        // Load the PDF document (using the required lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all form fields; filter only signature fields
            var signatureFields = doc.Form?.Fields?.OfType<SignatureField>() ?? Enumerable.Empty<SignatureField>();

            foreach (SignatureField sigField in signatureFields)
            {
                // Extract the signing certificate as an X509Certificate2 object
                X509Certificate2 signingCert = sigField.ExtractCertificateObject();

                if (signingCert == null)
                {
                    Console.WriteLine($"No certificate found in signature field '{sigField.PartialName}'.");
                    continue;
                }

                // Build the certificate chain for the extracted certificate
                X509Chain chain = new X509Chain();
                chain.Build(signingCert);

                // Save each certificate in the chain as a DER file
                for (int i = 0; i < chain.ChainElements.Count; i++)
                {
                    X509Certificate2 cert = chain.ChainElements[i].Certificate;
                    byte[] derBytes = cert.Export(X509ContentType.Cert); // DER‑encoded

                    // File name includes the signature field name and the chain index
                    string fileName = $"{sigField.PartialName}_cert_{i}.der";
                    string filePath = Path.Combine(outputDir, fileName);

                    File.WriteAllBytes(filePath, derBytes);
                    Console.WriteLine($"Saved certificate {i} of field '{sigField.PartialName}' to '{filePath}'.");
                }
            }
        }

        Console.WriteLine("Certificate extraction completed.");
    }
}
