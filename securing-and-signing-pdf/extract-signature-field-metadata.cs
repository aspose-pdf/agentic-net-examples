using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

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
                // Check if the document contains any form fields
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No form fields found in the PDF.");
                    return;
                }

                bool foundSignature = false;

                // Iterate over all form fields (SignatureField objects are stored in Form.Fields)
                foreach (Field field in doc.Form.Fields)
                {
                    if (field is SignatureField sigField)
                    {
                        foundSignature = true;
                        // Use PartialName for the field identifier (Signature.Name does not exist)
                        Console.WriteLine($"Signature Field Name: {sigField.PartialName}");

                        // Extract the certificate attached to the signature, if any
                        X509Certificate2 cert = sigField.ExtractCertificateObject();
                        if (cert != null)
                        {
                            Console.WriteLine($"  Subject        : {cert.Subject}");
                            Console.WriteLine($"  Issuer         : {cert.Issuer}");
                            Console.WriteLine($"  Valid From     : {cert.NotBefore}");
                            Console.WriteLine($"  Valid To       : {cert.NotAfter}");
                        }
                        else
                        {
                            Console.WriteLine("  No certificate data present.");
                        }
                    }
                }

                if (!foundSignature)
                {
                    Console.WriteLine("No signature fields found in the PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
