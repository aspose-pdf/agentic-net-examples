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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve all signature fields from the form (if any)
            var signatureFields = doc.Form?.Fields?.OfType<SignatureField>();

            if (signatureFields == null || !signatureFields.Any())
            {
                Console.WriteLine("No digital signatures found in the document.");
                return;
            }

            int sigIndex = 1;
            foreach (SignatureField sigField in signatureFields)
            {
                // Each field contains a Signature object with metadata
                Signature signature = sigField.Signature;

                // Reason and Location may be null, provide a fallback string
                string reason   = signature?.Reason   ?? "(no reason)";
                string location = signature?.Location ?? "(no location)";

                Console.WriteLine($"Signature {sigIndex}: Reason = \"{reason}\", Location = \"{location}\"");
                sigIndex++;
            }
        }
    }
}
