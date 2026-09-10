using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed_document.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Prepare validation options to use CRL distribution points
            ValidationOptions options = new ValidationOptions
            {
                ValidationMethod = ValidationMethod.Crl,      // Use CRL for revocation checking
                ValidationMode   = ValidationMode.Strict,    // Fail the signature if validation fails
                CheckCertificateChain = false               // Only check revocation status
            };

            // Iterate over all signature fields in the document
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // The actual signature object (PKCS7, PKCS7Detached, etc.)
                    Signature signature = sigField.Signature;

                    // Verify the signature using the prepared options
                    bool isValid = signature.Verify(options, out ValidationResult result);

                    // Output verification details
                    Console.WriteLine($"Signature field: {sigField.PartialName}");
                    Console.WriteLine($"  Valid: {isValid}");
                    Console.WriteLine($"  Validation result: {result?.Status}");
                    // RevocationInfo property is not available in the current Aspose.PDF version;
                    // if needed, additional revocation details can be obtained from the Status enum.
                    Console.WriteLine();
                }
            }
        }
    }
}
