using System;
using System.IO;
using System.Linq; // Added for Count() extension method
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "signed_document.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form with signature fields
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No digital signatures found in the document.");
                return;
            }

            // Collect all signature fields from the generic field collection
            var signatureFields = new System.Collections.Generic.List<SignatureField>();
            foreach (var field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                    signatureFields.Add(sigField);
            }

            if (signatureFields.Count == 0)
            {
                Console.WriteLine("No digital signatures found in the document.");
                return;
            }

            Console.WriteLine($"Found {signatureFields.Count} signature field(s):");

            // Iterate over each signature field and display its details
            foreach (SignatureField sigField in signatureFields)
            {
                // The actual signature object associated with the field
                Signature signature = sigField.Signature;

                // Signer identity (Authority) and timestamp (Date)
                string signer = signature.Authority ?? "(unknown authority)";
                DateTime? timestamp = signature.Date;

                Console.WriteLine($"- Field Name   : {sigField.PartialName}");
                Console.WriteLine($"  Signer       : {signer}");
                Console.WriteLine($"  Timestamp    : {(timestamp.HasValue ? timestamp.Value.ToString("u") : "(no timestamp)")}");

                // Optional additional information
                if (!string.IsNullOrEmpty(signature.Reason))
                    Console.WriteLine($"  Reason       : {signature.Reason}");
                if (!string.IsNullOrEmpty(signature.Location))
                    Console.WriteLine($"  Location     : {signature.Location}");
                Console.WriteLine();
            }
        }
    }
}
