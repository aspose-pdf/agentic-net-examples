using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";
        const string outputJson = "verification_results.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        using (Document document = new Document(inputPdf))
        {
            // Retrieve all signature fields from the PDF form
            var signatureFields = document.Form?.Fields?.OfType<SignatureField>();
            var verificationResults = new List<Dictionary<string, object>>();

            foreach (var sigField in signatureFields ?? Enumerable.Empty<SignatureField>())
            {
                // Default values
                bool isValid = false;
                string sigName = sigField?.PartialName ?? sigField?.Name ?? "Unnamed";

                // The actual Signature object provides the Verify() method
                var signature = sigField?.Signature;
                if (signature != null)
                {
                    try
                    {
                        isValid = signature.Verify();
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Verification error for signature '{sigName}': {ex.Message}");
                        isValid = false;
                    }
                }

                verificationResults.Add(new Dictionary<string, object>
                {
                    ["SignatureName"] = sigName,
                    ["IsValid"] = isValid
                });
            }

            string json = JsonSerializer.Serialize(verificationResults, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJson, json);
            Console.WriteLine($"Verification results written to '{outputJson}'.");
        }
    }
}
