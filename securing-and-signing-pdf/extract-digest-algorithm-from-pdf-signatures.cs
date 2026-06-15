using System;
using System.IO;
using System.Linq; // Needed for Count() / Any()
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
            // Ensure the document contains form fields
            if (doc.Form == null || doc.Form.Fields == null || !doc.Form.Fields.Any())
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            bool anySignature = false;
            // Iterate over all fields and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    anySignature = true;

                    // Retrieve algorithm information from the embedded signature
                    SignatureAlgorithmInfo algoInfo = sigField.Signature.GetSignatureAlgorithmInfo();
                    DigestHashAlgorithm digestAlg = algoInfo.DigestHashAlgorithm;

                    // Log the result for compliance reporting
                    Console.WriteLine($"Signature field '{sigField.PartialName}': Digest Algorithm = {digestAlg}");
                }
            }

            if (!anySignature)
            {
                Console.WriteLine("No digital signatures found in the document.");
            }
        }
    }
}
