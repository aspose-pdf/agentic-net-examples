using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "signed_document.pdf";
        const string outputJsonPath = "signature_verification_log.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Bind the PDF for signature operations
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdfPath);

            // Prepare a list to hold verification results
            var verificationResults = new List<SignatureResult>();

            // Check if the document contains any signatures
            if (pdfSignature.ContainsSignature())
            {
                // Retrieve names of all non‑empty signatures (returns IList<SignatureName>)
                IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);

                foreach (SignatureName sigNameObj in signatureNames)
                {
                    // Verify each signature using the overload that accepts SignatureName
                    bool isValid = pdfSignature.VerifySignature(sigNameObj);

                    verificationResults.Add(new SignatureResult
                    {
                        SignatureName = sigNameObj.Name,
                        IsValid = isValid
                    });
                }
            }
            else
            {
                Console.WriteLine("No signatures found in the document.");
            }

            // Serialize results to a structured JSON file
            using (FileStream fs = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
            {
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                JsonSerializer.Serialize(fs, verificationResults, options);
            }

            Console.WriteLine($"Signature verification log saved to '{outputJsonPath}'.");
        }
    }

    // Simple DTO for JSON serialization
    private class SignatureResult
    {
        public string SignatureName { get; set; } = string.Empty; // non‑null default
        public bool IsValid { get; set; }
    }
}
