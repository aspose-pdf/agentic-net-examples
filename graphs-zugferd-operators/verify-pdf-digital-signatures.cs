using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Signatures;
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
            // 1. Check for compromised signatures using SignaturesCompromiseDetector
            var detector = new SignaturesCompromiseDetector(doc);
            if (detector.Check(out CompromiseCheckResult compromiseResult))
            {
                Console.WriteLine("No compromised signatures detected.");
            }
            else
            {
                Console.WriteLine("Compromised signatures detected!");
                Console.WriteLine($"HasCompromisedSignatures: {compromiseResult.HasCompromisedSignatures}");
            }

            // 2. Iterate over signature fields and verify each signature
            var signatureFields = doc?.Form?.Fields?.OfType<SignatureField>()?.ToList();
            if (signatureFields != null && signatureFields.Count > 0)
            {
                foreach (var sigField in signatureFields)
                {
                    var signature = sigField.Signature;
                    if (signature == null)
                    {
                        Console.WriteLine($"Signature field '{sigField.PartialName}' does not contain a signature.");
                        continue;
                    }

                    // Basic verification
                    bool isValid = signature.Verify();
                    Console.WriteLine($"Signature field '{sigField.PartialName}': Valid = {isValid}");

                    // Strict verification with options
                    var options = new ValidationOptions
                    {
                        ValidationMode = ValidationMode.Strict,
                        CheckCertificateChain = true
                    };
                    if (signature.Verify(options, out ValidationResult validationResult))
                    {
                        Console.WriteLine("  Strict verification succeeded.");
                    }
                    else
                    {
                        Console.WriteLine("  Strict verification failed.");
                    }

                    // Retrieve signer details from the signature object
                    Console.WriteLine($"  Authority : {signature.Authority}");
                    Console.WriteLine($"  Date      : {signature.Date}");
                    Console.WriteLine($"  Reason    : {signature.Reason}");
                    Console.WriteLine($"  Location  : {signature.Location}");
                    Console.WriteLine($"  Contact   : {signature.ContactInfo}");
                }
            }
            else
            {
                Console.WriteLine("No signature fields found in the document.");
            }
        }
    }
}
