using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Signatures;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // 1. Check for compromised signatures
            SignaturesCompromiseDetector compromiseDetector = new SignaturesCompromiseDetector(doc);
            bool notCompromised = compromiseDetector.Check(out CompromiseCheckResult compromiseResult);

            Console.WriteLine($"Compromise check passed: {notCompromised}");
            Console.WriteLine($"Has compromised signatures: {compromiseResult.HasCompromisedSignatures}");

            // 2. Retrieve the first signature field (if any) via Form.Fields collection
            SignatureField sigField = null;
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sf)
                {
                    sigField = sf;
                    break; // we only need the first one for this demo
                }
            }

            if (sigField != null)
            {
                Signature signature = sigField.Signature;

                // Verify the signature using the core API (no ValidationOptions class exists)
                bool isValid = signature.Verify();

                Console.WriteLine($"Signature valid: {isValid}");

                // 3. Output signer details from the signature object
                Console.WriteLine($"Authority: {signature.Authority}");
                Console.WriteLine($"Date: {signature.Date}");
                Console.WriteLine($"Reason: {signature.Reason}");
                Console.WriteLine($"Location: {signature.Location}");
            }
            else
            {
                Console.WriteLine("No signature fields found in the document.");
            }
        }
    }
}
