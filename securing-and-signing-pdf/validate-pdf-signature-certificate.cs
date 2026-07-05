using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all fields and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    Console.WriteLine($"Found signature field: {sigField.PartialName}");

                    // The actual signature object attached to the field
                    Signature signature = sigField.Signature;

                    // Set up validation options (strict chain validation)
                    ValidationOptions validationOptions = new ValidationOptions
                    {
                        ValidationMode = ValidationMode.Strict,
                        CheckCertificateChain = true
                    };

                    // Verify the signature
                    bool isValid = signature.Verify(validationOptions, out ValidationResult validationResult);
                    Console.WriteLine($"Signature valid: {isValid}");

                    // Try to obtain certificate information using reflection – this works across
                    // different Aspose.Pdf versions where the API surface may have changed.
                    bool certInfoPrinted = false;

                    if (validationResult != null)
                    {
                        // Look for a property named "CertificateInfo"
                        PropertyInfo certInfoProp = validationResult.GetType().GetProperty("CertificateInfo");
                        if (certInfoProp != null)
                        {
                            object certInfo = certInfoProp.GetValue(validationResult);
                            if (certInfo != null)
                            {
                                PrintCertificateInfo(certInfo);
                                certInfoPrinted = true;
                            }
                        }
                    }

                    // Fallback – try to get an X509Certificate2 directly from the Signature object
                    if (!certInfoPrinted && signature != null)
                    {
                        PropertyInfo certProp = signature.GetType().GetProperty("Certificate");
                        if (certProp != null)
                        {
                            var cert = certProp.GetValue(signature) as X509Certificate2;
                            if (cert != null)
                            {
                                Console.WriteLine("Signing Certificate Details (from Signature):");
                                PrintX509Certificate(cert);
                                certInfoPrinted = true;
                            }
                        }
                    }

                    if (!certInfoPrinted)
                    {
                        Console.WriteLine("No certificate information available for this signature.");
                    }
                }
            }
        }
    }

    // Helper to print properties of the Aspose.Pdf.Security.CertificateInfo object via reflection
    private static void PrintCertificateInfo(object certInfo)
    {
        Console.WriteLine("Signing Certificate Details (from ValidationResult):");
        PrintProperty(certInfo, "Subject");
        PrintProperty(certInfo, "Issuer");
        PrintProperty(certInfo, "SerialNumber");
        PrintProperty(certInfo, "NotBefore");
        PrintProperty(certInfo, "NotAfter");
        PrintProperty(certInfo, "Thumbprint");
    }

    // Helper to print a single property safely
    private static void PrintProperty(object obj, string propertyName)
    {
        PropertyInfo prop = obj.GetType().GetProperty(propertyName);
        if (prop != null)
        {
            object value = prop.GetValue(obj);
            Console.WriteLine($"  {propertyName}: {value}");
        }
    }

    // Helper to print an X509Certificate2 instance
    private static void PrintX509Certificate(X509Certificate2 cert)
    {
        Console.WriteLine($"  Subject: {cert.Subject}");
        Console.WriteLine($"  Issuer: {cert.Issuer}");
        Console.WriteLine($"  Serial Number: {cert.SerialNumber}");
        Console.WriteLine($"  Not Before: {cert.NotBefore}");
        Console.WriteLine($"  Not After : {cert.NotAfter}");
        Console.WriteLine($"  Thumbprint: {cert.Thumbprint}");
    }
}
