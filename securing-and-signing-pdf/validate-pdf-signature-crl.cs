using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed_document.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Retrieve all signature fields from the AcroForm.
            var signatureFields = doc.Form?.Fields?.OfType<SignatureField>() ?? Enumerable.Empty<SignatureField>();

            foreach (SignatureField sigField in signatureFields)
            {
                object validationResult = null;

                try
                {
                    // Try to find an overload that accepts ValidationOptions (may not exist in older versions).
                    var methodWithOptions = sigField.GetType().GetMethod("ValidateSignature", new[] { typeof(object) });
                    if (methodWithOptions != null)
                    {
                        // Attempt to create ValidationOptions via reflection.
                        var optionsType = Type.GetType("Aspose.Pdf.Signatures.ValidationOptions, Aspose.Pdf");
                        object optionsInstance = null;
                        if (optionsType != null)
                        {
                            optionsInstance = Activator.CreateInstance(optionsType);
                            var checkRevocationProp = optionsType.GetProperty("CheckRevocation");
                            checkRevocationProp?.SetValue(optionsInstance, true);
                        }
                        validationResult = methodWithOptions.Invoke(sigField, new[] { optionsInstance });
                    }
                    else
                    {
                        // Fallback to parameter‑less overload (available in all supported versions).
                        var methodNoOptions = sigField.GetType().GetMethod("ValidateSignature", Type.EmptyTypes);
                        validationResult = methodNoOptions?.Invoke(sigField, null);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error validating signature '{sigField.PartialName}': {ex.Message}");
                    continue;
                }

                if (validationResult != null)
                {
                    // Use dynamic to access members without compile‑time type dependencies.
                    dynamic result = validationResult;
                    Console.WriteLine($"Signature '{sigField.PartialName}': Valid = {result.IsValid}");

                    if (result.RevocationInfo != null)
                    {
                        Console.WriteLine($"  Revocation status: {result.RevocationInfo.Status}");
                        Console.WriteLine($"  Revocation source: {result.RevocationInfo.Source}");
                    }
                    else
                    {
                        Console.WriteLine("  No revocation information available.");
                    }

                    Console.WriteLine($"  Signer: {result.SignerInfo?.Subject}");
                    Console.WriteLine($"  Signing time: {result.SigningTime}");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"Signature '{sigField.PartialName}' could not be validated (method not found).\n");
                }
            }
        }
    }
}
