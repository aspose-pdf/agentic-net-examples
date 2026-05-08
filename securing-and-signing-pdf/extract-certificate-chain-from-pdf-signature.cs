using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";
        const string outputDir = "certificates";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (lifecycle rule: use using)
            using (Document doc = new Document(inputPdf))
            {
                // Iterate over all fields and process only signature fields
                foreach (Field field in doc.Form.Fields)
                {
                    if (field is SignatureField sigField)
                    {
                        // Extract the leaf certificate as an X509Certificate2 object
                        X509Certificate2 leafCert = sigField.ExtractCertificateObject();
                        if (leafCert == null)
                        {
                            Console.WriteLine($"No certificate found in signature field '{sigField.PartialName}'.");
                            continue;
                        }

                        // Build the certificate chain for the leaf certificate
                        X509Chain chain = new X509Chain();
                        chain.Build(leafCert);

                        // Save each certificate in the chain as a DER file
                        int index = 0;
                        foreach (X509ChainElement element in chain.ChainElements)
                        {
                            string certPath = Path.Combine(outputDir,
                                $"{Path.GetFileNameWithoutExtension(inputPdf)}_sig_{sigField.PartialName}_cert_{index}.der");

                            // Write the raw DER bytes to file (simplified with WriteAllBytes)
                            File.WriteAllBytes(certPath, element.Certificate.RawData);

                            Console.WriteLine($"Saved certificate {index} of signature '{sigField.PartialName}' to '{certPath}'.");
                            index++;
                        }
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