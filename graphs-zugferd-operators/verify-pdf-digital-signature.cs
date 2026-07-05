using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Signatures;
using Aspose.Pdf.Security;

class VerifyPdfSignature
{
    static void Main()
    {
        const string pdfPath = "signed_document.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(pdfPath))
        {
            // 1. Check for compromised signatures using SignaturesCompromiseDetector
            SignaturesCompromiseDetector detector = new SignaturesCompromiseDetector(doc);
            CompromiseCheckResult compromiseResult;
            bool notCompromised = detector.Check(out compromiseResult);

            Console.WriteLine($"Compromise check passed: {notCompromised}");
            if (compromiseResult.HasCompromisedSignatures)
            {
                Console.WriteLine("Warning: Compromised signatures detected in the document.");
            }

            // 2. Iterate over all signature fields to verify each signature and retrieve signer details
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // The Signature object may be null if the field is empty
                    var signature = sigField.Signature;
                    if (signature == null)
                    {
                        Console.WriteLine($"Signature field '{sigField.PartialName}' is empty.");
                        continue;
                    }

                    // Basic verification (returns true if the signature is cryptographically valid)
                    bool basicValid = signature.Verify();
                    Console.WriteLine($"Signature field '{sigField.PartialName}': Basic verification = {basicValid}");

                    // Retrieve signer details from the signature object
                    Console.WriteLine($"  Authority : {signature.Authority}");
                    Console.WriteLine($"  Date      : {signature.Date}");
                    Console.WriteLine($"  Reason    : {signature.Reason}");
                    Console.WriteLine($"  Location  : {signature.Location}");

                    // Optional: perform strict validation with certificate chain checking
                    ValidationOptions validationOptions = new ValidationOptions
                    {
                        ValidationMode = ValidationMode.Strict,
                        CheckCertificateChain = true
                    };
                    ValidationResult validationResult;
                    bool strictValid = signature.Verify(validationOptions, out validationResult);
                    Console.WriteLine($"  Strict validation = {strictValid}");
                    Console.WriteLine($"  Validation status : {validationResult.Status}");
                }
            }
        }
    }
}
