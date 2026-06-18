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
            // Iterate over all fields and filter for signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // The Signature object contains the metadata (Reason, Location, etc.)
                    Signature signature = sigField.Signature;

                    string reason   = signature?.Reason   ?? "(no reason)";
                    string location = signature?.Location ?? "(no location)";

                    Console.WriteLine($"Signature Field: {sigField.FullName}");
                    Console.WriteLine($"  Reason:   {reason}");
                    Console.WriteLine($"  Location: {location}");
                    Console.WriteLine();
                }
            }
        }
    }
}
