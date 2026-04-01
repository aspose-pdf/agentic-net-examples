using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputJson = "verification_results.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            // Bind the document to the signature facade
            PdfFileSignature pdfSignature = new PdfFileSignature(document);

            // Retrieve all signature names present in the document (new API returns IList<SignatureName>)
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames();
            List<VerificationResult> results = new List<VerificationResult>();

            foreach (SignatureName sigInfo in signatureNames)
            {
                // Verify using the overload that accepts a SignatureName instance
                bool isValid = pdfSignature.VerifySignature(sigInfo);

                VerificationResult vr = new VerificationResult
                {
                    Signature = sigInfo.Name, // expose the string name for JSON output
                    IsValid = isValid
                };
                results.Add(vr);
            }

            string json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJson, json);
            Console.WriteLine($"Verification results saved to '{outputJson}'.");
        }
    }

    public class VerificationResult
    {
        public string Signature { get; set; }
        public bool IsValid { get; set; }
    }
}
