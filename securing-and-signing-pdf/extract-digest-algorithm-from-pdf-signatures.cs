using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            bool anySignature = false;

            // Iterate over form fields and process only signature fields that contain a signature
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    anySignature = true;

                    // Retrieve information about the signature algorithm
                    SignatureAlgorithmInfo algoInfo = sigField.Signature.GetSignatureAlgorithmInfo();

                    // Extract the digest hash algorithm used for the signature
                    DigestHashAlgorithm digestAlg = algoInfo.DigestHashAlgorithm;

                    // Log the signature field name and its digest algorithm
                    Console.WriteLine($"Signature field '{sigField.PartialName}' uses digest algorithm: {digestAlg}");
                }
            }

            if (!anySignature)
            {
                Console.WriteLine("No digital signatures found in the document.");
            }
        }
    }
}
