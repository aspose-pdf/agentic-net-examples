using System;
using System.IO;
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the collection of form fields and filter for signature fields
            bool anySignature = false;
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    anySignature = true;
                    // Retrieve algorithm information from the embedded signature
                    var algoInfo = sigField.Signature.GetSignatureAlgorithmInfo();
                    var digestAlg = algoInfo.DigestHashAlgorithm;

                    // Log the result; ToString() provides the enum name (e.g., Sha256)
                    Console.WriteLine($"Signature '{sigField.PartialName}': Digest algorithm = {digestAlg}");
                }
            }

            if (!anySignature)
            {
                Console.WriteLine("No digital signatures found in the document.");
            }
        }
    }
}
