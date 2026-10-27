using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve all form fields and filter for signature fields
            var signatureFields = doc?.Form?.Fields?.OfType<SignatureField>();
            if (signatureFields == null || !signatureFields.Any())
            {
                Console.WriteLine("No digital signatures found in the PDF.");
                return;
            }

            int sigIndex = 1;
            foreach (SignatureField sigField in signatureFields)
            {
                // The PKCS7 object attached to the signature field holds Reason and Location
                var pkcs7 = sigField.Signature;
                string reason   = pkcs7?.Reason   ?? "(none)";
                string location = pkcs7?.Location ?? "(none)";

                Console.WriteLine($"Signature {sigIndex}: Reason = \"{reason}\", Location = \"{location}\"");
                sigIndex++;
            }
        }
    }
}
