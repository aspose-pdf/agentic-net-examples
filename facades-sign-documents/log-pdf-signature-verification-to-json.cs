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

        // Initialize the PdfFileSignature facade and bind the PDF.
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdfPath);

            // Prepare a list to hold verification results.
            var verificationResults = new List<SignatureResult>();

            // Check if the document contains any signatures.
            if (pdfSignature.ContainsSignature())
            {
                // Retrieve the names of all present (non‑empty) signatures.
                // The boolean parameter indicates whether to include empty fields; false skips them.
                IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(false);

                foreach (SignatureName sigInfo in signatureNames)
                {
                    // The SignatureName object contains the actual field name in its Name property.
                    string sigName = sigInfo.Name;

                    // Verify each signature; VerifySignature returns true if the signature is valid.
                    bool isValid = pdfSignature.VerifySignature(sigName);

                    verificationResults.Add(new SignatureResult
                    {
                        SignatureName = sigName,
                        IsValid = isValid
                    });
                }
            }

            // Serialize the results to JSON with indentation for readability.
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(verificationResults, jsonOptions);

            // Write the JSON log to the specified file.
            File.WriteAllText(outputJsonPath, json);
        }

        Console.WriteLine($"Signature verification log written to '{outputJsonPath}'.");
    }

    // Helper class representing a single signature verification entry.
    private class SignatureResult
    {
        // Initialise with an empty string to satisfy non‑nullable analysis.
        public string SignatureName { get; set; } = string.Empty;
        public bool IsValid { get; set; }
    }
}
