using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace PdfSignatureVerification
{
    // Simple DTO for JSON serialization
    public class SignatureInfo
    {
        // Provide a default value to satisfy the non‑nullable warning
        public string Name { get; set; } = string.Empty;
        public bool IsValid { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "input.pdf";               // PDF to verify
            const string jsonReportPath = "signatures_report.json"; // Output JSON file

            // Ensure the input file exists
            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"File not found: {inputPdfPath}");
                return;
            }

            // Use PdfFileSignature facade to work with signatures
            using (PdfFileSignature pdfSignature = new PdfFileSignature())
            {
                // Bind the PDF document
                pdfSignature.BindPdf(inputPdfPath);

                var signatures = new List<SignatureInfo>();

                // Check whether the document contains any signatures
                if (pdfSignature.ContainsSignature())
                {
                    // Retrieve all non‑empty signature names (returns IList<SignatureName>)
                    IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);

                    // Verify each signature individually
                    foreach (SignatureName sig in signatureNames)
                    {
                        // The Name property holds the string representation of the signature field
                        string sigName = sig.Name;
                        // Use the newer VerifySignature method (VerifySigned is obsolete)
                        bool isValid = pdfSignature.VerifySignature(sigName);
                        signatures.Add(new SignatureInfo
                        {
                            Name = sigName,
                            IsValid = isValid
                        });
                    }
                }

                // Serialize the results to a formatted JSON string
                string json = JsonSerializer.Serialize(
                    signatures,
                    new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the specified file
                File.WriteAllText(jsonReportPath, json);

                // Optional explicit close (Dispose is called by using)
                pdfSignature.Close();
            }

            Console.WriteLine($"Signature verification report saved to '{jsonReportPath}'.");
        }
    }
}
