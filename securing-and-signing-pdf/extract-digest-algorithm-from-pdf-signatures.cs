using System;
using System.IO;
using System.Linq;
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

        using (Document doc = new Document(inputPath))
        {
            // Use Count() (extension method) because FieldCollection exposes Count as a method, not a property
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            bool anySignature = false;
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    anySignature = true;
                    // Retrieve algorithm information for the current signature
                    SignatureAlgorithmInfo algoInfo = sigField.Signature.GetSignatureAlgorithmInfo();
                    DigestHashAlgorithm digestAlg = algoInfo.DigestHashAlgorithm;

                    string sigName = string.IsNullOrEmpty(sigField.PartialName) ? "(unnamed)" : sigField.PartialName;
                    Console.WriteLine($"Signature '{sigName}' uses digest algorithm: {digestAlg}");
                }
            }

            if (!anySignature)
            {
                Console.WriteLine("No digital signatures found in the document.");
            }
        }
    }
}
