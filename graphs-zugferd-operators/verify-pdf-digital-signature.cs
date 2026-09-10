using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Signatures;

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(pdfPath))
        {
            // ------------------------------------------------------------
            // 1. Check the document for compromised signatures
            // ------------------------------------------------------------
            SignaturesCompromiseDetector detector = new SignaturesCompromiseDetector(doc);
            bool notCompromised = detector.Check(out CompromiseCheckResult compromiseResult);

            Console.WriteLine($"Compromise check passed: {notCompromised}");
            Console.WriteLine($"Has compromised signatures: {compromiseResult.HasCompromisedSignatures}");

            // ------------------------------------------------------------
            // 2. Iterate over all signature fields and retrieve signer info
            // ------------------------------------------------------------
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // The field may contain a signature object (PKCS7, etc.)
                    Signature signature = sigField.Signature;

                    if (signature == null)
                    {
                        Console.WriteLine($"Signature field '{sigField.PartialName}' is empty.");
                        continue;
                    }

                    // Basic verification of the signature
                    bool isValid = signature.Verify();
                    Console.WriteLine($"Signature field '{sigField.PartialName}' verification result: {isValid}");

                    // Cast to PKCS7 to access common signer properties
                    if (signature is PKCS7 pkcs7)
                    {
                        Console.WriteLine($"  Authority   : {pkcs7.Authority}");
                        Console.WriteLine($"  Date        : {pkcs7.Date}");
                        Console.WriteLine($"  Reason      : {pkcs7.Reason}");
                        Console.WriteLine($"  Location    : {pkcs7.Location}");
                        Console.WriteLine($"  ContactInfo : {pkcs7.ContactInfo}");
                    }
                }
            }
        }
    }
}
