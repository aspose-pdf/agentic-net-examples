using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security; // for DigestHashAlgorithm enum

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve all signature fields from the document's form collection
                var signatureFields = doc.Form?.Fields?.OfType<SignatureField>();

                if (signatureFields == null || !signatureFields.Any())
                {
                    Console.WriteLine("No digital signatures found in the document.");
                }
                else
                {
                    // Iterate over each signature field and log its digest algorithm
                    foreach (var sigField in signatureFields)
                    {
                        // The Signature property returns an abstract Signature instance.
                        // In practice it is a PKCS7 (or PKCS1) object. The digest algorithm
                        // used for timestamping is exposed via TimestampSettings.DigestHashAlgorithm.
                        string algorithm = "Unknown";
                        var pkcs = sigField.Signature as PKCS7;
                        if (pkcs?.TimestampSettings != null)
                        {
                            algorithm = pkcs.TimestampSettings.DigestHashAlgorithm.ToString();
                        }

                        string name = sigField.PartialName ?? "(unnamed)";
                        Console.WriteLine($"Signature '{name}' uses digest algorithm: {algorithm}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
