using System;
using System.IO;
using System.Linq;
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

        // Load the PDF document (digital signatures are preserved on load)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains an AcroForm with fields
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
                    // The Signature object holds the signing date (if the field is signed)
                    Signature signature = sigField.Signature;
                    if (signature == null)
                    {
                        Console.WriteLine($"Signature field '{sigField.PartialName}' is present but not signed.");
                        continue;
                    }

                    // Original signing time as stored in the PDF (may be local time)
                    DateTime signingTime = signature.Date;

                    // Convert the signing time to UTC
                    DateTime signingTimeUtc = signingTime.ToUniversalTime();

                    Console.WriteLine($"Signature field '{sigField.PartialName}':");
                    Console.WriteLine($"  Original (local) date: {signingTime:O}");
                    Console.WriteLine($"  UTC date: {signingTimeUtc:O}");
                }
            }
        }
    }
}
