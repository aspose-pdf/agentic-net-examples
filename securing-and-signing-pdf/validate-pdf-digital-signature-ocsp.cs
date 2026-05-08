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

        // Verify the input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the document contains form fields (signature fields are stored here)
            if (doc.Form == null || doc.Form.Fields == null || !doc.Form.Fields.Any())
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Configure validation options to use OCSP
            var options = new ValidationOptions
            {
                ValidationMethod = ValidationMethod.Ocsp,
                ValidationMode   = ValidationMode.Strict,
                // We only need revocation status; skip full chain checking
                CheckCertificateChain = false,
                // Timeout for network calls (milliseconds)
                RequestTimeout = 5000
            };

            int signatureIndex = 0;
            // Iterate over all fields and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    signatureIndex++;
                    // Verify the signature using the OCSP options
                    bool isValid = sigField.Signature.Verify(options, out ValidationResult validationResult);

                    // Output the verification result and the detailed status
                    Console.WriteLine($"Signature {signatureIndex} (field '{sigField.PartialName}'): Valid = {isValid}, Status = {validationResult.Status}");
                }
            }

            if (signatureIndex == 0)
            {
                Console.WriteLine("No digital signatures found in the document.");
            }
        }
    }
}
