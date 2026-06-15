using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through form fields and process signature fields only
            bool anySignature = false;
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField && sigField.Signature != null)
                {
                    anySignature = true;
                    // Prepare validation options
                    ValidationOptions validationOptions = new ValidationOptions
                    {
                        CheckCertificateChain = true,
                        ValidationMode = ValidationMode.Strict
                    };

                    // Verify the signature
                    bool isValid = sigField.Signature.Verify(validationOptions, out ValidationResult validationResult);

                    Console.WriteLine($"Signature field: {sigField.PartialName}");
                    Console.WriteLine($"  Valid: {isValid}");

                    if (validationResult != null)
                    {
                        // Report detailed validation information if available
                        Console.WriteLine($"  Validation result: {validationResult}");
                        // Some versions expose an ErrorMessage property – output it when present
                        var errorMsgProp = validationResult.GetType().GetProperty("ErrorMessage");
                        if (errorMsgProp != null)
                        {
                            string errorMsg = errorMsgProp.GetValue(validationResult) as string;
                            if (!string.IsNullOrEmpty(errorMsg))
                                Console.WriteLine($"  Error message: {errorMsg}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  No detailed validation result returned.");
                    }

                    Console.WriteLine();
                }
            }

            if (!anySignature)
            {
                Console.WriteLine("No digital signature fields found in the document.");
            }
        }
    }
}
