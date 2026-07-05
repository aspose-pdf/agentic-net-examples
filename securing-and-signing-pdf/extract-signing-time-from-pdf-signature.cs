using System;
using System.IO;
using System.Linq; // Needed for Count() and Any()
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form with fields
            if (doc.Form == null || doc.Form.Fields == null || !doc.Form.Fields.Any())
            {
                Console.WriteLine("No form fields (including signatures) found in the document.");
                return;
            }

            // Iterate through each field and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // The field name (partial name) can be used for identification
                    string fieldName = sigField.PartialName;

                    // Retrieve the Signature object attached to the field
                    Signature signature = sigField.Signature;
                    if (signature == null)
                    {
                        Console.WriteLine($"Signature field '{fieldName}' does not contain a signature.");
                        continue;
                    }

                    // Get the signing time (as stored in the signature)
                    DateTime signingTime = signature.Date;

                    // Convert the signing time to UTC
                    DateTime utcSigningTime = signingTime.ToUniversalTime();

                    // Output the result in ISO‑8601 format
                    Console.WriteLine($"Signature '{fieldName}': Signing time (UTC) = {utcSigningTime:O}");
                }
            }
        }
    }
}
