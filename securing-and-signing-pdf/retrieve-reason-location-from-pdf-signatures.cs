using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Signatures;

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
            // Iterate over all form fields and filter for signature fields
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // The underlying PKCS#7 signature (if present)
                    PKCS7 pkcs7 = sigField.Signature as PKCS7;

                    string reason   = pkcs7?.Reason   ?? "(no reason)";
                    string location = pkcs7?.Location ?? "(no location)";

                    Console.WriteLine($"Signature Field: {sigField.PartialName}");
                    Console.WriteLine($"  Reason:   {reason}");
                    Console.WriteLine($"  Location: {location}");
                    Console.WriteLine();
                }
            }
        }
    }
}
