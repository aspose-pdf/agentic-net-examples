using System;
using System.IO;
using System.Linq; // Needed for Count() extension method
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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form with fields
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            bool signatureFound = false;
            // Iterate over all fields and process those that are signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // Retrieve the underlying Signature object (may be null if not signed yet)
                    Signature signature = sigField.Signature;
                    if (signature == null)
                    {
                        Console.WriteLine($"Signature field '{sigField.PartialName}' is present but not signed.");
                        continue;
                    }

                    // The Date property holds the signing time (usually in local time)
                    DateTime signingTimeLocal = signature.Date;
                    // Convert the signing time to UTC
                    DateTime signingTimeUtc = signingTimeLocal.ToUniversalTime();

                    Console.WriteLine($"Signature field: {sigField.PartialName}");
                    Console.WriteLine($"Signing time (local): {signingTimeLocal:O}");
                    Console.WriteLine($"Signing time (UTC)  : {signingTimeUtc:O}");
                    signatureFound = true;
                }
            }

            if (!signatureFound)
            {
                Console.WriteLine("No digital signatures found in the document.");
            }
        }
    }
}
