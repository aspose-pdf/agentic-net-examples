using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "signed_document.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve all signature fields via the generic Fields collection
            var signatureFields = doc.Form?.Fields?.OfType<SignatureField>();

            if (signatureFields == null || !signatureFields.Any())
            {
                Console.WriteLine("No digital signatures found in the document.");
                return;
            }

            // Iterate over each signature field and output its Reason and Location
            foreach (SignatureField sigField in signatureFields)
            {
                Console.WriteLine($"Signature Field: {sigField.PartialName}");

                // The Signature property holds the signed data after the field is signed.
                var signature = sigField.Signature;
                if (signature != null)
                {
                    // Reason, Location and ContactInfo are stored on the underlying PKCS7 object.
                    // They are exposed through the Signature object's properties.
                    Console.WriteLine($"  Reason  : {signature.Reason ?? "(none)"}");
                    Console.WriteLine($"  Location: {signature.Location ?? "(none)"}");
                }
                else
                {
                    Console.WriteLine("  (field not signed)");
                }

                Console.WriteLine();
            }
        }
    }
}
