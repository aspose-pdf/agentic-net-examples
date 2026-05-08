using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Set up validation options (strict mode, check certificate chain)
            ValidationOptions valOptions = new ValidationOptions
            {
                ValidationMode = ValidationMode.Strict,
                CheckCertificateChain = true
            };

            // Iterate over all form fields and handle signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    Console.WriteLine($"Signature field: {sigField.PartialName}");

                    // Verify the signature using the validation options
                    bool isValid = false;
                    ValidationResult valResult;
                    try
                    {
                        isValid = sigField.Signature.Verify(valOptions, out valResult);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Verification error: {ex.Message}");
                    }

                    Console.WriteLine($"Signature valid: {isValid}");

                    // Extract the embedded signing certificate
                    X509Certificate2 cert = sigField.ExtractCertificateObject();
                    if (cert != null)
                    {
                        Console.WriteLine($"Certificate Subject: {cert.Subject}");

                        // Look for the Subject Alternative Name extension (OID 2.5.29.17)
                        X509Extension sanExtension = cert.Extensions["2.5.29.17"];
                        if (sanExtension != null)
                        {
                            // Decode and display the SAN values
                            AsnEncodedData asnData = new AsnEncodedData(sanExtension.Oid, sanExtension.RawData);
                            string sanString = asnData.Format(true);
                            Console.WriteLine("Subject Alternative Names:");
                            Console.WriteLine(sanString);
                        }
                        else
                        {
                            Console.WriteLine("No Subject Alternative Name extension present.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No certificate found in the signature.");
                    }
                }
            }
        }
    }
}