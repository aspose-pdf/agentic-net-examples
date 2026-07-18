using System;
using System.IO;
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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form (AcroForm) – signatures are stored as signature fields
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields (including signatures) found in the document.");
                return;
            }

            bool anySignature = false;

            // Iterate over each field and process only signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    anySignature = true;
                    // The underlying PKCS#7 object holds the Reason and Location information
                    PKCS7 pkcs7 = sigField.Signature as PKCS7;
                    string reason   = pkcs7?.Reason   ?? "(no reason)";
                    string location = pkcs7?.Location ?? "(no location)";

                    Console.WriteLine($"Signature field: {sigField.PartialName}");
                    Console.WriteLine($"  Reason  : {reason}");
                    Console.WriteLine($"  Location: {location}");
                    Console.WriteLine();
                }
            }

            if (!anySignature)
            {
                Console.WriteLine("No digital signatures found in the document.");
            }
        }
    }
}
