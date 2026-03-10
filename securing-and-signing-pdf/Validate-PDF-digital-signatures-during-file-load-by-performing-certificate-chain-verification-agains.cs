using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Security;
using Aspose.Pdf.Signatures;

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
            // Initialize the facade for signature operations
            PdfFileSignature pdfSignature = new PdfFileSignature(doc);

            // Check if the document contains any digital signatures
            if (!pdfSignature.ContainsSignature())
            {
                Console.WriteLine("The PDF does not contain any digital signatures.");
                return;
            }

            // Retrieve all signature names (including empty fields if needed)
            // GetSignatureNames now returns IList<SignatureName>
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);

            // Configure validation options: enforce certificate‑chain checking and strict mode
            ValidationOptions validationOptions = new ValidationOptions
            {
                CheckCertificateChain = true,
                ValidationMode = ValidationMode.Strict
            };

            // Verify each signature against the trusted authorities using the new overload
            foreach (SignatureName sigName in signatureNames)
            {
                bool isValid = pdfSignature.VerifySignature(sigName, validationOptions, out ValidationResult validationResult);
                // SignatureName exposes the actual name via the Name property
                Console.WriteLine($"Signature '{sigName.Name}': Valid = {isValid}");
                // Additional details can be inspected via validationResult if required
            }

            // Optional: check for signature compromise using the detector
            SignaturesCompromiseDetector compromiseDetector = new SignaturesCompromiseDetector(doc);
            bool compromiseCheckPassed = compromiseDetector.Check(out CompromiseCheckResult compromiseResult);
            Console.WriteLine($"Compromise check passed: {compromiseCheckPassed}");
            Console.WriteLine($"Has compromised signatures: {compromiseResult.HasCompromisedSignatures}");
        }
    }
}
