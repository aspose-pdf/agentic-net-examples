using System;
using System.IO;
using System.Linq; // Needed for Count() extension method
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form with fields
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            bool anySignature = false;

            // Iterate over each field and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    anySignature = true;
                    // Retrieve algorithm information for the current signature
                    SignatureAlgorithmInfo algoInfo = sigField.Signature.GetSignatureAlgorithmInfo();

                    // The DigestHashAlgorithm field indicates the hash algorithm used
                    DigestHashAlgorithm digestAlg = algoInfo.DigestHashAlgorithm;

                    // Log the result (signature field name and its digest algorithm)
                    // Use null‑conditional operator for safety, although PartialName is unlikely to be null
                    Console.WriteLine($"Signature field '{sigField.PartialName ?? "<unknown>"}': Digest Algorithm = {digestAlg}");
                }
            }

            if (!anySignature)
            {
                Console.WriteLine("No digital signatures found in the document.");
            }
        }
    }
}
