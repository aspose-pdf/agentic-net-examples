using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains form fields
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Length == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            bool anySignature = false;

            // Iterate over all form fields
            foreach (Field field in doc.Form.Fields)
            {
                // Identify signature fields
                if (field is SignatureField sigField)
                {
                    anySignature = true;

                    // Basic field information
                    Console.WriteLine("=== Signature Field ===");
                    Console.WriteLine($"Field Name   : {sigField.Name}");
                    Console.WriteLine($"Partial Name : {sigField.PartialName}");
                    // SignatureField exposes PageIndex (0‑based). Convert to 1‑based for display.
                    Console.WriteLine($"Page Index   : {sigField.PageIndex + 1}");

                    // The associated Signature object holds metadata
                    Signature signature = sigField.Signature;

                    if (signature != null)
                    {
                        // Use the properties that actually exist on Signature
                        Console.WriteLine($"Signer Name  : {sigField.PartialName}"); // field name as identifier
                        Console.WriteLine($"Reason       : {signature.Reason}");
                        Console.WriteLine($"Location     : {signature.Location}");
                        Console.WriteLine($"Contact Info : {signature.ContactInfo}");
                        Console.WriteLine($"Signing Date : {signature.Date:u}");
                        Console.WriteLine($"Authority    : {signature.Authority}");
                        // Verify returns a bool indicating whether the signature is valid
                        Console.WriteLine($"Signature Valid : {signature.Verify()}");
                    }
                    else
                    {
                        Console.WriteLine("No signature data attached.");
                    }

                    Console.WriteLine(); // Blank line for readability
                }
            }

            if (!anySignature)
            {
                Console.WriteLine("No signature fields found in the document.");
            }
        }
    }
}
