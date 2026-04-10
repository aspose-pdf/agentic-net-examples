using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

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

        using (Document doc = new Document(pdfPath))
        {
            // Ensure the document contains form fields (signature fields are stored here)
            if (doc.Form == null || doc.Form.Fields == null || !doc.Form.Fields.Any())
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Validation options – use only CRL distribution points
            ValidationOptions options = new ValidationOptions
            {
                ValidationMethod = ValidationMethod.Crl,   // CRL based revocation checking
                ValidationMode   = ValidationMode.Strict, // treat any failure as invalid
                CheckCertificateChain = false            // we only need revocation status
            };

            int signatureIndex = 0;
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    signatureIndex++;

                    // The embedded signature object
                    Signature signature = sigField.Signature;
                    if (signature == null)
                    {
                        Console.WriteLine($"Signature {signatureIndex}: no signature data present.");
                        continue;
                    }

                    // Verify using the configured options
                    bool isValid = signature.Verify(options, out ValidationResult validationResult);

                    Console.WriteLine($"Signature {signatureIndex} (field '{sigField.PartialName}'): {(isValid ? "VALID" : "INVALID")}");

                    if (validationResult != null)
                    {
                        // ValidationResult does not expose an ErrorMessage property in the current API version.
                        // Use its string representation to obtain diagnostic information.
                        Console.WriteLine($"  Validation details: {validationResult}");
                    }
                }
            }

            if (signatureIndex == 0)
                Console.WriteLine("No digital signature fields found in the document.");
        }
    }
}
