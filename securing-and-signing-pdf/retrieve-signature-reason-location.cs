using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // SignatureField and Signature are defined here

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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields and filter for signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // The actual signature object associated with the field (may be null if not signed yet)
                    Aspose.Pdf.Forms.Signature signature = sigField.Signature;

                    // Retrieve the Reason and Location properties (guard against null signatures)
                    string reason   = signature?.Reason ?? "<none>";
                    string location = signature?.Location ?? "<none>";

                    Console.WriteLine($"Signature field: {sigField.FullName}");
                    Console.WriteLine($"  Reason:   {reason}");
                    Console.WriteLine($"  Location: {location}");
                }
            }
        }
    }
}
