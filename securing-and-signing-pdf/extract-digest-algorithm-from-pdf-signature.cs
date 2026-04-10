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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each signature field in the document
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    // Retrieve information about the signature algorithm
                    var algoInfo = sigField.Signature.GetSignatureAlgorithmInfo();

                    // DigestHashAlgorithm is a non‑nullable enum, so we cannot use the null‑conditional operator.
                    // First ensure algoInfo is not null, then read the enum value.
                    string digestAlgorithm = "Unknown";
                    if (algoInfo != null)
                    {
                        digestAlgorithm = algoInfo.DigestHashAlgorithm.ToString();
                    }

                    // Log the digest algorithm (e.g., Sha256, Sha1, etc.)
                    Console.WriteLine($"Signature field '{sigField.PartialName}' uses digest algorithm: {digestAlgorithm}");
                }
            }
        }
    }
}
