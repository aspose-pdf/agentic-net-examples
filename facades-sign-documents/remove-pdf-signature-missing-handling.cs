using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputPath = "unsigned.pdf";
        const string signatureName = "Signature1";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Bind the document to the signature handler
                using (PdfFileSignature pdfSignature = new PdfFileSignature())
                {
                    pdfSignature.BindPdf(doc);

                    // Get all existing signature field names (IList<SignatureName>)
                    IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames();

                    // Convert to a simple string array for display purposes (optional)
                    string[] signatureNameStrings = new string[signatureNames.Count];
                    for (int i = 0; i < signatureNames.Count; i++)
                        signatureNameStrings[i] = signatureNames[i].Name;

                    // Determine whether the requested signature exists
                    bool exists = false;
                    SignatureName targetSignature = null;
                    foreach (SignatureName name in signatureNames)
                    {
                        if (string.Equals(name.Name, signatureName, StringComparison.OrdinalIgnoreCase))
                        {
                            exists = true;
                            targetSignature = name;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        // Signature not found – report available signatures
                        Console.WriteLine($"Signature '{signatureName}' not found. Available signatures: {string.Join(", ", signatureNameStrings)}");
                    }
                    else
                    {
                        // Optional: display reason and location, handling possible missing data
                        try
                        {
                            // GetReason/GetLocation accept a SignatureName object
                            string reason = pdfSignature.GetReason(targetSignature);
                            string location = pdfSignature.GetLocation(targetSignature);
                            Console.WriteLine($"Removing signature '{signatureName}'. Reason: '{reason}', Location: '{location}'.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Could not retrieve reason/location for signature '{signatureName}': {ex.Message}");
                        }

                        // Remove the signature field
                        pdfSignature.RemoveSignature(signatureName);
                        Console.WriteLine($"Signature '{signatureName}' removed.");
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
