using System;
using System.IO;
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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all fields and filter for signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // Retrieve the underlying Signature object (may be null if not signed yet)
                    Signature signature = sigField.Signature;
                    if (signature == null)
                    {
                        Console.WriteLine($"Signature field '{sigField.PartialName}' is not signed.");
                        continue;
                    }

                    // Get the signing time (local time as stored in the PDF)
                    DateTime localSigningTime = signature.Date;

                    // Convert to UTC
                    DateTime utcSigningTime = localSigningTime.ToUniversalTime();

                    Console.WriteLine($"Signature field '{sigField.PartialName}':");
                    Console.WriteLine($"  Local signing time : {localSigningTime:O}");
                    Console.WriteLine($"  UTC signing time   : {utcSigningTime:O}");
                }
            }
        }
    }
}
