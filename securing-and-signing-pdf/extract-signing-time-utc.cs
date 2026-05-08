using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the signed PDF document
        const string inputPdf = "signed_document.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document contains at least one signature field
            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            SignatureField sigField = null;
            // Iterate over form fields and pick the first SignatureField
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sField)
                {
                    sigField = sField;
                    break; // use the first signature field found
                }
            }

            if (sigField == null)
            {
                Console.WriteLine("No digital signatures found in the document.");
                return;
            }

            // The Signature object associated with the field
            Signature signature = sigField.Signature;
            if (signature == null)
            {
                Console.WriteLine("Signature data is missing for the field.");
                return;
            }

            // Extract the signing time (local time as stored in the PDF)
            DateTime signingTimeLocal = signature.Date;

            // Convert the signing time to UTC
            DateTime signingTimeUtc = signingTimeLocal.ToUniversalTime();

            // Output the result
            Console.WriteLine($"Signing time (local): {signingTimeLocal:O}");
            Console.WriteLine($"Signing time (UTC)  : {signingTimeUtc:O}");
        }
    }
}
