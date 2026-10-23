using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace PdfSignatureVerification
{
    // Simple DTO for JSON output
    public class SignatureResult
    {
        public string SignatureName { get; set; }
        public bool IsValid { get; set; }

        // Parameter‑less constructor ensures non‑nullable property is initialized
        public SignatureResult()
        {
            SignatureName = string.Empty;
        }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "signed_document.pdf";
            const string jsonOutputPath = "signature_verification.json";

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                return;
            }

            try
            {
                // Bind the PDF to the signature facade
                PdfFileSignature pdfSignature = new PdfFileSignature();
                pdfSignature.BindPdf(inputPdfPath);

                // Retrieve all signature names present in the document (compatible with all Aspose.Pdf versions)
                IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames();

                var results = new List<SignatureResult>();

                foreach (SignatureName sigName in signatureNames)
                {
                    // Verify each signature; returns true if the signature is valid
                    bool isValid = pdfSignature.VerifySignature(sigName);
                    results.Add(new SignatureResult
                    {
                        SignatureName = sigName.Name,
                        IsValid = isValid
                    });
                }

                // Serialize results to formatted JSON
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(results, jsonOptions);

                // Write JSON to file
                using (FileStream fs = new FileStream(jsonOutputPath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(json);
                }

                Console.WriteLine($"Signature verification completed. Results saved to '{jsonOutputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during verification: {ex.Message}");
            }
        }
    }
}
