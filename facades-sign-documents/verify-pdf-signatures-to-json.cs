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
        const string inputPdfPath = "signed_document.pdf";
        const string outputJsonPath = "signature_verification_results.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document and initialize the PdfFileSignature facade
        using (Document doc = new Document(inputPdfPath))
        using (PdfFileSignature pdfSignature = new PdfFileSignature(doc))
        {
            // Retrieve all signature names (including empty fields if needed)
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);

            var results = new List<SignatureResult>();

            foreach (SignatureName sig in signatureNames)
            {
                // Verify each signature using the overload that accepts SignatureName
                bool isValid = pdfSignature.VerifySignature(sig);
                results.Add(new SignatureResult { Name = sig.Name, IsValid = isValid });
            }

            // Serialize results to JSON with indentation for readability
            string json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the specified output file
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Signature verification results saved to '{outputJsonPath}'.");
        }
    }

    // Helper class representing a single signature verification entry
    private class SignatureResult
    {
        public string Name { get; set; } = string.Empty;
        public bool IsValid { get; set; }
    }
}
