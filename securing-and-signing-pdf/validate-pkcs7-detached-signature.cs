using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;
using System.Security.Cryptography.X509Certificates;

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Iterate over all fields and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // Retrieve the attached signature object; expect PKCS7Detached
                    var pkcs7Detached = sigField.Signature as PKCS7Detached;
                    if (pkcs7Detached == null)
                    {
                        Console.WriteLine($"Signature field '{sigField.PartialName}' does not contain a PKCS7Detached signature.");
                        continue;
                    }

                    // Configure validation options to check the certificate chain
                    ValidationOptions options = new ValidationOptions
                    {
                        CheckCertificateChain = true,
                        ValidationMode = ValidationMode.Strict
                    };

                    // Verify the signature using the configured options
                    bool isValid = pkcs7Detached.Verify(options, out ValidationResult validationResult);

                    Console.WriteLine($"Signature field '{sigField.PartialName}': Valid = {isValid}");
                    if (validationResult != null)
                    {
                        // Output basic validation result information
                        Console.WriteLine($"  Validation status: {validationResult.Status}");
                    }
                }
            }
        }
    }
}
