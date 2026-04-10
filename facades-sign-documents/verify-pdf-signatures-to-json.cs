using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfSignatureVerification
{
    // Simple DTO for JSON serialization
    public class SignatureVerificationResult
    {
        // Initialise to avoid CS8618 warning for non‑nullable reference types
        public string SignatureName { get; set; } = string.Empty;
        public bool IsValid { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string outputJsonPath = "verification_results.json";

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                return;
            }

            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize the facade for signature operations
                PdfFileSignature signatureFacade = new PdfFileSignature();
                signatureFacade.BindPdf(pdfDocument); // bind the loaded document

                var verificationResults = new List<SignatureVerificationResult>();

                // Check if the document contains any signatures
                if (signatureFacade.ContainsSignature())
                {
                    // Retrieve names of all signatures (returns IList<SignatureName>)
                    IList<SignatureName> signatureNames = signatureFacade.GetSignatureNames(false);

                    foreach (SignatureName sigInfo in signatureNames)
                    {
                        // The actual string name of the signature field
                        string sigName = sigInfo.Name;

                        // Verify each signature using the non‑obsolete API
                        bool isValid = signatureFacade.VerifySignature(sigName);

                        verificationResults.Add(new SignatureVerificationResult
                        {
                            SignatureName = sigName,
                            IsValid = isValid
                        });
                    }
                }

                // Serialize the results to a pretty‑printed JSON string
                string json = JsonSerializer.Serialize(
                    verificationResults,
                    new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON to the specified output file
                File.WriteAllText(outputJsonPath, json);
                Console.WriteLine($"Verification results saved to '{outputJsonPath}'.");
            }
        }
    }
}
