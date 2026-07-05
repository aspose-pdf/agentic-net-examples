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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            bool anySignature = false;
            int sigIndex = 1;

            // Iterate over all form fields and pick the signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    anySignature = true;
                    // The Signature object holds the metadata such as Reason and Location
                    var signature = sigField.Signature;
                    string reason   = signature?.Reason   ?? "(no reason)";
                    string location = signature?.Location ?? "(no location)";

                    Console.WriteLine($"Signature {sigIndex}:");
                    Console.WriteLine($"  Reason   : {reason}");
                    Console.WriteLine($"  Location : {location}");
                    sigIndex++;
                }
            }

            if (!anySignature)
            {
                Console.WriteLine("No digital signatures found in the document.");
            }
        }
    }
}
