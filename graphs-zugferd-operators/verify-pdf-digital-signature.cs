using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Signatures;

class VerifyPdfSignature
{
    static void Main()
    {
        const string inputPath = "signed_document.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Check for compromised signatures using SignaturesCompromiseDetector
            // -----------------------------------------------------------------
            var detector = new SignaturesCompromiseDetector(doc);
            bool signaturesNotCompromised = detector.Check(out CompromiseCheckResult compromiseResult);

            Console.WriteLine($"Compromise check passed: {signaturesNotCompromised}");
            Console.WriteLine($"Has compromised signatures: {compromiseResult.HasCompromisedSignatures}");

            // -----------------------------------------------------------------
            // 2. Iterate over all signature fields and verify each signature
            // -----------------------------------------------------------------
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    // The underlying Signature object (PKCS7, PKCS1, etc.)
                    Signature signature = sigField.Signature;

                    // Verify the signature (basic verification without external certificate)
                    bool isValid = signature.Verify();

                    // Retrieve signer details from the signature properties
                    string authority = signature.Authority ?? "(unknown)";
                    string date = signature.Date.ToString("u"); // Date is non‑nullable
                    string reason = signature.Reason ?? "(none)";
                    string location = signature.Location ?? "(unknown)";

                    Console.WriteLine("----- Signature Details -----");
                    Console.WriteLine($"Field name : {sigField.PartialName}");
                    Console.WriteLine($"Valid      : {isValid}");
                    Console.WriteLine($"Authority  : {authority}");
                    Console.WriteLine($"Date       : {date}");
                    Console.WriteLine($"Reason     : {reason}");
                    Console.WriteLine($"Location   : {location}");
                    Console.WriteLine();
                }
            }
        }
    }
}
