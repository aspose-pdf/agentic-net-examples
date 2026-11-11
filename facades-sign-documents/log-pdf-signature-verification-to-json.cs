using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

// Input PDF containing digital signatures
const string inputPdfPath = "signed_document.pdf";
// Output JSON file that will store verification results
const string outputJsonPath = "signature_verification_results.json";

if (!File.Exists(inputPdfPath))
{
    Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
    return;
}

// Prepare a list to hold the verification results
var verificationResults = new List<object>();

// PdfFileSignature does not implement IDisposable, so no using block is required
PdfFileSignature pdfSignature = new PdfFileSignature();
pdfSignature.BindPdf(inputPdfPath);

// Retrieve all signature names present in the PDF
IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);

foreach (SignatureName sigName in signatureNames)
{
    // Get signer name; fall back to the raw signature name if the signer name is empty
    string signer = pdfSignature.GetSignerName(sigName);
    if (string.IsNullOrEmpty(signer))
    {
        signer = sigName.Name;
    }

    // Verify the signature
    bool isValid = pdfSignature.VerifySignature(sigName);

    var result = new
    {
        SignatureName = signer,
        IsValid = isValid
    };
    verificationResults.Add(result);
}

// Serialize the results to a formatted JSON string
string json = JsonSerializer.Serialize(verificationResults, new JsonSerializerOptions
{
    WriteIndented = true
});

// Write the JSON to the output file
File.WriteAllText(outputJsonPath, json);

Console.WriteLine($"Signature verification results saved to '{outputJsonPath}'.");