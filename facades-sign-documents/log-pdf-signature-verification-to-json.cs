using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;          // PdfFileSignature, SignatureName

// Model for a single verification result
public class SignatureVerificationResult
{
    // Mark as required (C# 11) so the compiler knows it will be set before use
    public required string SignatureName { get; set; }
    public bool IsValid { get; set; }
}

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

        // Initialise the facade that works with digital signatures
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPdfPath);

        // Get all signature fields (both signed and empty). The method now returns
        // IList<SignatureName> – each item contains the field's Name property.
        IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);

        var results = new List<SignatureVerificationResult>();

        foreach (SignatureName sigInfo in signatureNames)
        {
            // If the field does not actually contain a signature, VerifySignature will
            // simply return false – we can still log it.
            bool isValid = pdfSignature.VerifySignature(sigInfo);

            results.Add(new SignatureVerificationResult
            {
                SignatureName = sigInfo.Name,
                IsValid = isValid
            });
        }

        // Serialize the list to a nicely‑indented JSON document
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(results, jsonOptions);

        File.WriteAllText(outputJsonPath, json);

        // Clean‑up
        pdfSignature.Close();

        Console.WriteLine($"Signature verification log saved to '{outputJsonPath}'.");
    }
}
